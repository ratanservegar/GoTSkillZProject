using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTSkillZ.Application.Transforms.DTO
{
  public class SqlParameterDTO
    {
        /// <summary>
        ///     SP Parameter
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        ///     Parameter Value
        /// </summary>
        public object Value { get; set; }
    }
}
