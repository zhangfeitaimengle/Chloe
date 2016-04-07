﻿using Chloe.Query.DbExpressions;
using Chloe.Query.Implementation;
using Chloe.Query.Mapping;
using Chloe.Query.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Query.QueryState
{
    /// <summary>
    ///  ps:该类的部分属性和方法有 bug ，多次调用 GetResultElement() 会得到意想不到的结果
    /// </summary>
    abstract class BaseQueryState : IQueryState
    {
        protected BaseQueryState()
        {
        }
        public virtual IQueryState AppendWhereExpression(WhereExpression whereExp)
        {
            throw new NotImplementedException();
        }
        public virtual IQueryState AppendOrderExpression(OrderExpression orderExp)
        {
            throw new NotImplementedException();
        }

        public virtual ResultElement Result
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public virtual IQueryState UpdateSelectResult(SelectExpression selectExpression)
        {
            throw new NotImplementedException();
        }


        //protected void VisistOrderExpressions(BaseExpressionVisitor visitor, List<OrderPart> orderParts)
        //{
        //    foreach (OrderExpression orderExp in this.OrderExpressions)
        //    {
        //        OrderPart orderPart = VisistOrderExpression(visitor, orderExp);
        //        orderParts.Add(orderPart);
        //    }
        //}

        //protected DbExpression VisistWhereExpressions(BaseExpressionVisitor visitor)
        //{
        //    return VisistWhereExpressions(visitor, this.WhereExpressions);
        //}
        //protected static DbExpression VisistWhereExpressions(BaseExpressionVisitor visitor, IList<WhereExpression> whereExpressions)
        //{
        //    DbExpression ret = null;
        //    if (whereExpressions != null)
        //        foreach (WhereExpression whereExpression in whereExpressions)
        //        {
        //            DbExpression whereDbExpression = visitor.Visit(whereExpression.Expression);
        //            if (ret == null)
        //                ret = whereDbExpression;
        //            else
        //                ret = new DbAndExpression(ret, whereDbExpression);
        //        }

        //    return ret;
        //}
        protected static OrderPart VisistOrderExpression(BaseExpressionVisitor visitor, OrderExpression orderExp)
        {
            DbExpression dbExpression = visitor.Visit(orderExp.Expression);//解析表达式树 orderExp.Expression
            OrderType orderType;
            if (orderExp.NodeType == QueryExpressionType.OrderBy || orderExp.NodeType == QueryExpressionType.ThenBy)
            {
                orderType = OrderType.Asc;
            }
            else if (orderExp.NodeType == QueryExpressionType.OrderByDesc || orderExp.NodeType == QueryExpressionType.ThenByDesc)
            {
                orderType = OrderType.Desc;
            }
            else
                throw new NotSupportedException(orderExp.NodeType.ToString());

            OrderPart orderPart = new OrderPart(dbExpression, orderType);

            return orderPart;
        }

        public virtual MappingData GenerateMappingData()
        {
            throw new NotImplementedException();
        }

    }
}
