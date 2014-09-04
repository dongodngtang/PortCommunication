using System;
using System.Collections.Generic;
using FormUI.Filters;
using TomorrowSoft.BLL;
using TomorrowSoft.Model;

namespace FormUI.OperationLayer
{
    public class RecMesSave
    {
        public void SaveMes(string content ,string phone,string name)
        {
            if (content.Contains("喇叭"))
            {
                new ConditionService()
                    .Add(ConditionFilter.FilterCondition(phone, content,name));
            
            }
            else
            {

                new HistoryRecordService().Add(new HistoryRecord()
                {
                    Name = name ,
                    Handler = "收信",
                    PhoneNo = phone,
                    HandlerTime = DateTime.Now.ToLocalTime(),
                    Context = content,
                });

            }
        }
    }
}