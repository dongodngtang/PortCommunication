using System;
using TomorrowSoft.BLL;
using TomorrowSoft.Model;
using FormUI.Properties;
namespace FormUI.Filters
{
    public class ConditionFilter
    {
         private static string _photovoltaic;
        private static string _battery;
        public static string content;
        public static string[] Horn;
    
         public static Condition FilterCondition(string phone,string context,string name)
         {
             string[] result = context.Split(new[] { "光伏", "\r\n", "\n", "\r\t","电池",
             "市电","功放","1喇叭","2喇叭","3喇叭","4喇叭"}, 
             StringSplitOptions.RemoveEmptyEntries);
             var condition = new Condition()
                 {
                     PhoneNo = phone,
                     Battery = result[0],
                     Photovoltaic = result[1],
                     Name = name,
                     Horn1 = result[2],
                     Horn2 = result[3],
                     Horn3 = result[4],
                     Horn4 = result[5],
                     HandlerTime = DateTime.Now.ToLocalTime()
                     
                 };
             Horn = result;
             _battery = result[0].Replace( "V",string.Empty);
             _photovoltaic = result[1].Replace("V", string.Empty);

             return condition;

         }
        public static bool PhotovoltaicCompare(out string text)
        {
            text = string.Empty;
            var qs = new QsService().GetAll();
            var isHors = false;
            if (_photovoltaic == null || _battery == null) throw new ArgumentException("_photovoltaic和_battery不能为空！");  
            if (qs.Rows.Count>0)
            {
                if (double.Parse(_photovoltaic) < Convert.ToDouble(Settings.Default.RDS))
                {
                    text = string.Format("告警：光伏值：{0}低于{1}\r\n", _photovoltaic, Settings.Default.RDS);
                }
                if (double.Parse(_battery) < Convert.ToDouble(Settings.Default.Battery))
                {
                    text += string.Format("告警：电池值：{0}低于{1}\r\n", _battery, Settings.Default.Battery);
                }
                for(int a=2; a<=5;a++)
                {
                    if (Horn[a].Contains("0.0"))
                    {
                        text += string.Format("告警：{0}喇叭异常\r\n", a - 1);
                        isHors = true;
                    }
                    
                }
                return (((double.Parse(_photovoltaic) < Convert.ToDouble(Settings.Default.RDS)) &&
                        double.Parse(_battery) < Convert.ToDouble(Settings.Default.Battery))||isHors);

                
            }
            return false;
        }
    }
}