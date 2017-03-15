/*
 * Resilient Systems, Inc. ("Resilient") is willing to license software
 * or access to software to the company or entity that will be using or
 * accessing the software and documentation and that you represent as
 * an employee or authorized agent ("you" or "your") only on the condition
 * that you accept all of the terms of this license agreement.
 *
 * The software and documentation within Resilient's Development Kit are
 * copyrighted by and contain confidential information of Resilient. By
 * accessing and/or using this software and documentation, you agree that
 * while you may make derivative works of them, you:
 *
 * 1)  will not use the software and documentation or any derivative
 *     works for anything but your internal business purposes in
 *     conjunction your licensed used of Resilient's software, nor
 * 2)  provide or disclose the software and documentation or any
 *     derivative works to any third party.
 *
 * THIS SOFTWARE AND DOCUMENTATION IS PROVIDED "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL RESILIENT BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 */

// <auto-generated>
// Generated by <a href="http://enunciate.webcohesion.com">Enunciate</a>.
// </auto-generated>

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Co3.Rest.Dto
{
    /// <summary>
    ///  Represents a task object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TaskDto 
    {
        /// <summary>
        /// The name of the incident to which this task belongs.
        /// </summary>
        [JsonProperty("inc_name")]
        public string IncName { get; set; }

        /// <summary>
        /// The name of the task.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Information about the regulators that triggered this task.
        /// </summary>
        [JsonProperty("regs")]
        public TaskRegsDto RegsModel { get; set; }

        /// <summary>
        /// true for custom tasks; false for built-in tasks.  Custom tasks are those
        /// which are manually created on an incident.
        /// </summary>
        [JsonProperty("custom")]
        public bool CustomTask { get; set; }

        /// <summary>
        /// The ID of the incident to which this task belongs.  This is a readonly property.
        /// </summary>
        [JsonProperty("inc_id")]
        public int IncId { get; set; }

        /// <summary>
        /// The owner of the incident to which this task belongs.  This is a readonly property.
        /// </summary>
        [JsonProperty("inc_owner_id")]
        public ObjectHandle IncOwnerId { get; set; }

        /// <summary>
        /// The task due date.  null indicates that the task has no assigned due date.
        /// </summary>
        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// true if this task is required; false if it is optional.  This is a readonly property.
        /// </summary>
        [JsonProperty("required")]
        public bool Required { get; set; }

        /// <summary>
        /// The owner of the task.  null if the task has no owner.
        /// </summary>
        [JsonProperty("owner_id")]
        public ObjectHandle OwnerId { get; set; }


        /// <summary>
        /// The task's ID.  This is a readonly property.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The task's status.  The possible task status values are available
        /// in the <a href="json_ConstDTO.html#reslink_task_statuses">constDTO (task_statuses property)</a>.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// true if the incident to which this task belongs is a training incident (simulation).
        /// This is a readonly property.
        /// </summary>
        [JsonProperty("inc_training")]
        public bool TrainingIncident { get; set; }

        /// <summary>
        /// true if the incident to which this task belongs is "frozen" because of a
        /// legal hold.  This is a readonly property.
        /// </summary>
        [JsonProperty("frozen")]
        public bool Frozen { get; set; }

        /// <summary>
        /// The task owner's first name.  This is a readonly property.
        /// </summary>
        [JsonProperty("owner_fname")]
        public string OwnerFirstName { get; set; }

        /// <summary>
        /// The task owner's last name.  This is a readonly property.
        /// </summary>
        [JsonProperty("owner_lname")]
        public string OwnerLastName { get; set; }

        /// <summary>
        /// The name of the rollup (category) to which this task was assigned.
        /// This is a readonly property.
        /// </summary>
        [JsonProperty("cat_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// The description of the task.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The date this task was activated.  As an incident changes, it is possible for a task
        /// to become "inactive".  For example, if you originally say that 100 records were lost
        /// in Massachusetts then latter revised that number to 0, the Massachusetts notification
        /// tasks would be deactivated.  You could later reactivate the task by entering numbers
        /// in for Massachusetts.  They are not deleted because work may have been done on
        /// them.  So the init_date (activation date) would be either the date the task was originally
        /// created, or the date it was most recently activated.  This is a readonly property.
        /// </summary>
        [JsonProperty("init_date")]
        public DateTime InitDate { get; set; }

        [JsonProperty("src_name")]
        [Obsolete]
        public string SrcName { get; set; }

        /// <summary>
        /// The textual instructions for the task.  Note that this field only applies when
        /// instructions are added by the user.  This will override the default instructions
        /// for the task.  In order to get the default task instructions, you use the
        /// <a href="resource_TaskREST.html">TaskREST endpoint</a>.
        /// </summary>
        [JsonProperty("instr_text")]
        public string CustomInstructions { get; set; }

        /// <summary>
        /// The automatic task ID (for tasks created based on an "automatic task").  See
        /// the <a href="resource_AutomaticTaskREST.html">AutomaticTaskREST resource</a>
        /// for more information.
        /// 
        /// This value is being deprecated in v27 and will be removed in v28.  Applications using this
        /// property should switch to using the <a href="json_TaskDTO.html#reslink_at_id">at_id</a>
        /// property instead.
        /// </summary>
        [JsonProperty("auto_task_id")]
        [Obsolete("Deprecated in v27.0 and will be removed in v28.0")]
        public ObjectHandle DeprecatedAutoTaskId { get; set; }

        /// <summary>
        /// The automatic task ID (for tasks created based on an "automatic task").  See
        /// the <a href="resource_AutomaticTaskREST.html">AutomaticTaskREST resource</a>
        /// for more information.
        /// </summary>
        [JsonProperty("at_id")]
        public ObjectHandle AutoTaskId { get; set; }

        /// <summary>
        /// Is the task currently active?  A task can become inactive when incident information
        /// changes to make the task no longer applicable.  We do not delete the task in this case
        /// because work could have been done on the task.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }


        /// <summary>
        /// The list of task members.  If this value is null then the task is accessible by all
        /// incident members.
        /// </summary>
        [JsonProperty("members")]
        public List<ObjectHandle> Members { get; set; }

        /// <summary>
        /// Indicates that the current user's permissions are to various elements of the
        /// incident.  For example, is the current user allowed to change the membership
        /// of the incident (stored in the change_members property of the returned object).
        /// </summary>
        [JsonProperty("perms")]
        public TaskPermsDto Perms { get; set; }

        /// <summary>
        /// Gets a user object that describes the task creator.
        /// </summary>
        [JsonProperty("creator")]
        public JustUserDto Creator { get; set; }

        [JsonProperty("notes")]
        public List<CommentDto> Notes { get; set; }

        /// <summary>
        /// Get the date the task was closed. This field is read only.
        /// </summary>
        [JsonProperty("closed_date")]
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// The list of actions available to the caller for execution.
        /// </summary>
        [JsonProperty("actions")]
        public List<ActionInfoDto> Actions { get; set; }

        /// <summary>
        /// The phase to which this task belongs.
        /// 
        /// The <a href="json_FullOrgDTO.html#reslink_phases">FullOrgDTO (phases property)</a>
        /// contains the possible phase values.
        /// </summary>
        [JsonProperty("phase_id")]
        public ObjectHandle PhaseId { get; set; }

        /// <summary>
        /// The category to which this task belongs.
        /// 
        /// The <a href="json_FullOrgDTO.html#reslink_task_categories">FullOrgDTO (task_categories property)</a>
        /// contains the possible category values.
        /// </summary>
        [JsonProperty("category_id")]
        public ObjectHandle CategoryId { get; set; }

        /// <summary>
        /// Number of notes on this task
        /// </summary>
        [JsonProperty("notes_count")]
        public int NotesCount { get; set; }

        /// <summary>
        /// Number of attachments on this task
        /// </summary>
        [JsonProperty("attachments_count")]
        public int AttachmentsCount { get; set; }


    }
}
