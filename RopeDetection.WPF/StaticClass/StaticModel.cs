﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RopeDetection.WPF.StaticClass
{
    public static class StaticModel
    {
        public static Guid ModelId { get; set; }
        public static Guid TypeId { get; internal set; }
        public static string NameModel { get; internal set; }
        public static bool IsAllDataEntered { get; set; }
        public static string ErrorMessage { get; set; }
    }
}
