﻿using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Entities.Models
{
    public class PersonCourse
    {
        public int PersonId { get; set; }
        public int CourseId { get; set; }
        public Status Status { get; set; }

        public Course? Course { get; set; }
        public Person? Person { get; set; }
    }
}
