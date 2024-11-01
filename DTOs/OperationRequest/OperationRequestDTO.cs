﻿using System;

namespace DDDNetCore.DTOs.OperationRequest
{
    public class OperationRequestDTO
    {
        public Guid ID { get; set; }
        public string patientID { get; set; }
        public string doctorID { get; set; }
        public string operationTypeID { get; set; }
        public string operationDateTime { get; set; }
        public string deadline { get; set; }
        public int priority { get; set; }
        public OperationRequestDTO(Guid ID, string patientID, string doctorID, string operationTypeID, string operationDateTime, string deadline, int priority)
        {
            this.ID = ID;
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.operationDateTime = operationDateTime;
            this.deadline = deadline;
            this.priority = priority;
        }
    }
}
