#!/usr/bin/env python

# Resilient Systems, Inc. ("Resilient") is willing to license software
# or access to software to the company or entity that will be using or
# accessing the software and documentation and that you represent as
# an employee or authorized agent ("you" or "your") only on the condition
# that you accept all of the terms of this license agreement.
#
# The software and documentation within Resilient's Development Kit are
# copyrighted by and contain confidential information of Resilient. By
# accessing and/or using this software and documentation, you agree that
# while you may make derivative works of them, you:
#
# 1)  will not use the software and documentation or any derivative
#     works for anything but your internal business purposes in
#     conjunction your licensed used of Resilient's software, nor
# 2)  provide or disclose the software and documentation or any
#     derivative works to any third party.
#
# THIS SOFTWARE AND DOCUMENTATION IS PROVIDED "AS IS" AND ANY EXPRESS
# OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
# WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
# ARE DISCLAIMED. IN NO EVENT SHALL RESILIENT BE LIABLE FOR ANY DIRECT,
# INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
# (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
# SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
# HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
# STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
# ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
# OF THE POSSIBILITY OF SUCH DAMAGE.

from __future__ import print_function
import co3 as resilient
import json
import sys
import codecs
import csv
import collections
import StringIO

def wrap_io(stream):
    """Wrap the stream to always always output in utf-8"""
    if stream.encoding != 'UTF-8':
        if sys.version_info.major < 3:
            return codecs.getwriter('utf-8')(stream, 'strict')
        else:
            return codecs.getwriter('utf-8')(stream.buffer, 'strict')
    return stream

# Always always output in utf-8
sys.stdout = wrap_io(sys.stdout)
sys.stderr = wrap_io(sys.stderr)


class FinfoArgumentParser(resilient.ArgumentParser):
    def __init__(self):
        super(FinfoArgumentParser, self).__init__()

        self.add_argument('fieldname',
            nargs = "?",
            help = "The field name.")

        self.add_argument('--type',
            default = "incident",
            choices = ["incident", "task", "artifact", "milestone", "attachment", "note", "actioninvocation"],
            help = "The object type (defaults to 'incident')")

        self.add_argument('--json',
            action = 'store_true',
            help = "Print the field definition in JSON format.")

        self.add_argument('--csv',
            action = 'store_true',
            help = "Print the field lists in CSV format.")

def apiname(field):
    """The full (qualified) programmatic name of a field"""
    if field["prefix"]:
        fieldname = u"{}.{}".format(field["prefix"], field["name"])
    else:
        fieldname = field["name"]
    return fieldname

def print_json(field):
    """Print the definition of one field, in JSON"""
    print(json.dumps(field, indent=4))

def print_details(field):
    """Print the definition of one field, in readable text"""
    print(u"Name:        {}".format(apiname(field)))
    print(u"Label:       {}".format(field["text"]))
    print(u"Type:        {}".format(field["input_type"]))
    if "tooltip" in field:
        if field["tooltip"]:
            print(u"Tooltip:     {}".format(field["tooltip"]))
    if "placeholder" in field:
        if field["placeholder"]:
            print(u"Placeholder: {}".format(field["placeholder"]))
    if "required" in field:
        print(u"Required:    {}".format(field["required"]))
    if "values" in field:
        if field["values"]:
            print("Values:")
            v = sorted(field["values"], key=lambda x : x["value"])
            for value in v:
                default_flag = " "
                if value["default"]:
                    default_flag = "*"
                if not value["enabled"]:
                    default_flag = "x"
                label = value["label"]
                print (u'{} {}={}'.format(default_flag, value["value"], label))

def find_field(client, fieldname, objecttype="incident"):
    trimname = fieldname[fieldname.rfind(".")+1:]
    t = client.get("/types/{}/fields".format(objecttype))
    for field in t:
        if field["name"] == trimname:
            return field

def list_fields_csv(client, objecttype="incident"):
    """Print a list of fields, in CSV format"""
    iostr = StringIO.StringIO()
    writer = None
    t = client.get("/types/{}/fields".format(objecttype))
    for field in sorted(t, key=apiname):
        columns = collections.OrderedDict()
        columns["name"] = apiname(field)
        columns["required"] = field.get("required", "")
        columns["input_type"] = field.get("input_type", "")
        columns["text"] = field.get("text", "")
        columns["tooltip"] = field.get("tooltip", "")
        columns["placeholder"] = field.get("placeholder", "")
        if not writer:
            writer = csv.DictWriter(iostr, fieldnames=columns.keys(), dialect='excel')
            writer.writeheader()
        writer.writerow(columns)
    print(iostr.getvalue())

def list_fields(client, objecttype="incident"):
    """Print a list of fields, in readable text"""
    print("Fields:")
    t = client.get("/types/{}/fields".format(objecttype))
    for field in sorted(t, key=apiname):
        required_flag = " "
        if "required" in field:
            if field["required"] == "always":
                required_flag = "*"
            if field["required"] == "close":
                required_flag = "c"
        print(u"{} {}".format(required_flag, apiname(field)))


def main(argv):
    """Main"""
    # Parse commandline arguments
    parser = FinfoArgumentParser()
    opts = parser.parse_args()

    # Create SimpleClient and connect
    verify = True
    if opts.cafile:
        verify = opts.cafile
    url = "https://{}:{}".format(opts.host, opts.port)

    client = resilient.SimpleClient(org_name=opts.org, proxies=opts.proxy, base_url=url, verify=verify)
    client.connect(opts.email, opts.password)

    # If no field is specified, list them all
    if not opts.fieldname:
        if opts.csv:
            list_fields_csv(client, opts.type)
        else:
            list_fields(client, opts.type)
        exit(0)

    # Find the field and display its properties
    field_data = find_field(client, opts.fieldname, opts.type)
    if field_data:
        if opts.json:
            print_json(field_data)
        else:
            print_details(field_data)
        exit(0)
    else:
        print(u"Field '{}' was not found.".format(opts.fieldname))
        exit(1)

if __name__ == "__main__":
    main(sys.argv[1:])