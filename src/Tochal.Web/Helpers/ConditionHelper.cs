using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public class ConditionHelper<T> where T:class
    {
        private List<Expression<Func<T, bool>>> ConditionList { get; set; } = new List<Expression<Func<T, bool>>>();

        public void AddCondition(Expression<Func<T, bool>> condition)
        {
            ConditionList.Add(condition);
        }

        public List<Expression<Func<T, bool>>> GetConditionList()
        {
            return ConditionList;
        }

    }
}
