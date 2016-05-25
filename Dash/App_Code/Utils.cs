using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Web;

namespace Dash
{
    public static class Utils
    {
        public static DashUser GetUser(this object sessionUser)
        {
            DashUser user = new DashUser();

            if (sessionUser != null)
            {
                try
                {
                    user = (DashUser) sessionUser;
                }
                catch
                {
                    //user = null;
                }
            }

            return user;
        }

        public static T GetField<T>(this DataRow row, string field)
        {
            try
            {
                object value = row[field];

                if (row != null && row.Table.Columns.Contains(field))
                {
                    if (row.IsNull(field))
                    {
                        switch (Type.GetTypeCode(typeof(T)))
                        {
                            case TypeCode.DateTime:
                                value = DateTime.MinValue;
                                break;

                            case TypeCode.Boolean:
                                value = false;
                                break;

                            case TypeCode.Double:

                            case TypeCode.Decimal:

                            case TypeCode.Int16:

                            case TypeCode.Int32:

                            case TypeCode.Int64:
                                value = 0;
                                break;

                            case TypeCode.String:
                                value = "";
                                break;
                        }
                    }

                    return (T) Convert.ChangeType(value, typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return default(T);
        }

        public static string CreateCircleHtmlPercentage(string width = "60px",
    string fillColor = "#5ea6e1",
    string strokewidth = "7",
    string strokeColor = "#fff",
    decimal value = 0,
    decimal maxValue = 100,
    string textX = "50",
    string textY = "55",
    string fontSize = "30",
    string textColor = "#fff",
    string text = "")
        {
            decimal cPerc = 0;

            if (maxValue > 0)
                cPerc = (value / maxValue) * 100;

            decimal cValue = 0;

            if (cPerc > 0)
            {
                cValue = Convert.ToInt32((250 * cPerc) / 100);
            }

            if (text == "")
                text = ((int) cPerc).ToString() + "%";

            string circleString = " <svg id=\"svg\"  Height=\"" + width + "\"   width=\"" + width + "\" viewbox=\"0 0 100 100\"> " +
                                  "   <circle cx=\"50\" cy=\"50\" r=\"45\" fill=\"" + fillColor + "\"/> " +
                                  "   <path fill=\"none\" stroke-linecap=\"round\" stroke-width=\"" + strokewidth + "\" stroke=\"" + strokeColor + "\" " +
                                  "         stroke-dasharray=\"" + cValue + ",250\" " +
                                  "         d=\"M50 10 " +
                                  "            a 40 40 0 0 1 0 80 " +
                                  "            a 40 40 0 0 1 0 -80\"/> " +
                                  "   <text x=\"" + textX + "\" y=\"" + textY + "\" fill=\"" + textColor + "\"  text-anchor=\"middle\" dy=\"7\" font-family=\"'Open Sans', sans-serif\"  font-size=\"" + fontSize + "\">" +
                                  text + "</text> " +
                                  " </svg> ";

            return circleString;
        }

        public static string CreateCircleHtmlPercentageBoxed(string width = "60px",
            string fillColor = "#5ea6e1",
            string strokewidth = "7",
            string strokeColor = "#fff",
            decimal value = 0,
            decimal maxValue = 100,
            string textX = "50",
            string textY = "55",
            string fontSize = "30",
            string textColor = "#fff",
            string textcircle = "",
            string textfooter = "")
        {
            decimal cPerc = 0;

            if (maxValue > 0)
                cPerc = (value / maxValue) * 100;

            decimal cValue = 0;

            if (cPerc > 0)
            {
                cValue = Convert.ToInt32((250 * cPerc) / 100);
            }

            if (textcircle == "")
                textcircle = ((int) cPerc).ToString() + "%";

            string circleString = "<div class='pchContainer'> " +
                                  " <svg id=\"svg\"  Height=\"" + width + "\"   width=\"" + width + "\" viewbox=\"0 0 100 100\"> " +
                                  "   <circle cx=\"50\" cy=\"50\" r=\"45\" fill=\"" + fillColor + "\"/> " +
                                  "   <path fill=\"none\" stroke-linecap=\"round\" stroke-width=\"" + strokewidth + "\" stroke=\"" + strokeColor + "\" " +
                                  "         stroke-dasharray=\"" + cValue + ",250\" " +
                                  "         d=\"M50 10 " +
                                  "            a 40 40 0 0 1 0 80 " +
                                  "            a 40 40 0 0 1 0 -80\"/> " +
                                  "   <text x=\"" + textX + "\" y=\"" + textY + "\" fill=\"" + textColor + "\"  text-anchor=\"middle\" dy=\"7\" font-family=\"'Open Sans', sans-serif\"  font-size=\"" + fontSize + "\">" +
                                  textcircle + "</text> " +
                                  " </svg> " +
                                  "<div class='pchFooter'> " +
                                  textfooter +
                                  "</div>  </div>";

            return circleString;
        }

        public static DateTime ToDTM(string dateString)
        {
            DateTime dConverted = DateTime.MinValue;

            try
            {
                dConverted = Convert.ToDateTime(dateString);
            }
            catch (Exception)
            {
            }

            return dConverted;
        }

        public static Int32 ToInt32(this string value)
        {
            if (value == null || !IsNumeric(value))
                return 0;
            else
                return Convert.ToInt32(value);
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static T GetValue<T>(this NameValueCollection collection, string key, T dValue = default(T))
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            var value = collection[key];
            var typeOF = typeof(T);

            //if (value == null)
            //    value = "";

            var converter = TypeDescriptor.GetConverter(typeOF);

            if (!converter.CanConvertFrom(typeOF))
            {
                if (value == null & dValue != null)
                {
                    return dValue;
                }
                else if (value != null & dValue != null)
                {
                    try
                    {
                        switch (Type.GetTypeCode(typeOF))
                        {
                            case TypeCode.Int32:

                                return ((T) converter.ConvertTo(value, typeof(T)));

                            default:

                                return default(T);
                        }
                    }
                    catch (Exception)
                    {
                        return dValue;
                    }
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return (T) converter.ConvertFrom(value);
            }
        }
    }
}