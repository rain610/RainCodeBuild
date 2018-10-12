using System;
using System.Collections.Generic;
using System.Text;

namespace DBModel
{
    public class DoctorDosageModel
    {
        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 医院名称
        /// </summary>
        public string Hospital{ get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string OfficeName { get; set; }
        /// <summary>
        /// 用药量
        /// </summary>
        public int Dosage { get; set; }
        /// <summary>
        /// 药房用量
        /// </summary>
        public int DrugStore { get; set; }
        /// <summary>
        /// 医生费用
        /// </summary>
        public int Money { get; set; }
    }

    public class DoctorDosageStatistics
    {
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 医生用药量及费用情况
        /// </summary>
        public IList<DoctorDosageModel> DoctorDosageList { get; set; } = new List<DoctorDosageModel>();
    }
}
