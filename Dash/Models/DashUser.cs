using System;
using System.Globalization;
using System.Linq;

namespace Dash
{
    public class DashUser
    {
        public string CompnayName { get; set; }

        public string Country { get; set; }

        public string CountryId { get; set; }

        public string eMail { get; set; }

        public bool ExportIpAddress { get; set; }

        public string GalleryId { get; set; }

        public int ID { get; set; }

        public string LastLoginDate { get; set; }

        public bool LoginAllowed { get; set; }

        public string ManufacturerId { get; set; }

        public string PassWord { get; set; }

        public string RealName { get; set; }

        public bool ShowCouponMenu { get; set; }

        public bool ShowGalleryMenu { get; set; }

        public bool ShowNewsLetter { get; set; }

        //public bool ShowStatistics { get; set; }

        public string UserName { get; set; }

        public DashUser()
        {
            ID = 0;

            UserName = "";
            PassWord = "";

            ManufacturerId = "";
            CountryId = "";
            GalleryId = "";

            RealName = "";
            CompnayName = "";
            eMail = "";

            LoginAllowed = false;
            ShowGalleryMenu = false;
            ShowCouponMenu = false;
            //ShowStatistics = false;
            ShowNewsLetter = false;
            ExportIpAddress = false;

            Country = "";
        }

        public static DashUser GetUserByID(int id, bool updateLastLogin)
        {
            using (MsSqlHelper adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable(" SELECT TOP 1 [ID] " +
                                      " ,[UserName] " +
                                      " ,[PassWord] " +
                                      " ,[Country_Id] " +
                                      " ,[Manufacturer_Id] " +
                                      " ,[Gallery_Id] " +
                                      " ,[Real_Name] " +
                                      " ,[Company_Name] " +
                                      " ,[eMail] " +
                                      " ,[Login_Allowed] " +
                                      " ,[ShowGalleryMenu] " +
                                      " ,[ShowCouponMenu] " +
                                      " ,[ShowStatistics] " +
                                      " ,[ShowNewsLetter] " +
                                      " ,[StatDetails] " +
                                      " ,[LastLoginDate] " +
                                      " ,[ExportIpAddress] " +
                                      " FROM [P@H].[dbo].[Users] where id = " + id.ToString());

                return LoadUser(adoData, updateLastLogin);
            }
        }

        public static DashUser GetUserByUP(string userName, string passWord, bool updateLastLogin)
        {
            using (MsSqlHelper adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable(" SELECT TOP 1 [ID] " +
                                      " ,[UserName] " +
                                      " ,[PassWord] " +
                                      " ,[Country_Id] " +
                                      " ,[Manufacturer_Id] " +
                                      " ,[Gallery_Id] " +
                                      " ,[Real_Name] " +
                                      " ,[Company_Name] " +
                                      " ,[eMail] " +
                                      " ,[Login_Allowed] " +
                                      " ,[ShowGalleryMenu] " +
                                      " ,[ShowCouponMenu] " +
                                      " ,[ShowStatistics] " +
                                      " ,[ShowNewsLetter] " +
                                      " ,[StatDetails] " +
                                      " ,[LastLoginDate] " +
                                      " ,[ExportIpAddress] " +
                                      " FROM [P@H].[dbo].[Users] where UserName = '" + userName + "' and password = '" + passWord + "' ");

                return LoadUser(adoData, updateLastLogin);
            }
        }

        public bool Save()
        {
            using (MsSqlHelper adoData = new MsSqlHelper())
            {
                adoData.OpenDataTable(" SELECT TOP 1 [ID] " +
                                      " ,[UserName] " +
                                      " ,[PassWord] " +
                                      " ,[Country_Id] " +
                                      " ,[Manufacturer_Id] " +
                                      " ,[Gallery_Id] " +
                                      " ,[Real_Name] " +
                                      " ,[Company_Name] " +
                                      " ,[eMail] " +
                                      " ,[Login_Allowed] " +
                                      " ,[ShowGalleryMenu] " +
                                      " ,[ShowCouponMenu] " +
                                      " ,[ShowStatistics] " +
                                      " ,[ShowNewsLetter] " +
                                      " ,[StatDetails] " +
                                      " ,[LastLoginDate] " +
                                      " ,[ExportIpAddress] " +
                                      " FROM [P@H].[dbo].[Users] where id = " + this.ID.ToString());

                adoData.Data.Rows[0]["PassWord"] = this.PassWord;
                adoData.Data.Rows[0]["Real_Name"] = this.RealName;
                adoData.Data.Rows[0]["eMail"] = this.eMail;
                adoData.Update();

                return true;
            }
        }

        private static DashUser LoadUser(MsSqlHelper userData, bool updateLastLogin)
        {
            DashUser user = new DashUser();

            //RegionInfo cRegion = Utils.ResolveCountry();

            if (userData.Data.Rows.Count > 0)
            {
                //if (cRegion != null)
                //    user.Country = cRegion.ToString();

                user.ID = Convert.ToInt32(userData.Data.Rows[0]["ID"].ToString());
                user.UserName = userData.Data.Rows[0]["UserName"].ToString();
                user.PassWord = userData.Data.Rows[0]["PassWord"].ToString();

                user.CountryId = userData.Data.Rows[0]["Country_Id"].ToString() == "0" ? "" : userData.Data.Rows[0]["Country_Id"].ToString();
                user.ManufacturerId = userData.Data.Rows[0]["Manufacturer_Id"].ToString() == "0" ? "" : userData.Data.Rows[0]["Manufacturer_Id"].ToString();
                user.GalleryId = userData.Data.Rows[0]["Gallery_Id"].ToString() == "0" ? "" : userData.Data.Rows[0]["Gallery_Id"].ToString();

                user.RealName = userData.Data.Rows[0]["Real_Name"].ToString();
                user.CompnayName = userData.Data.Rows[0]["Company_Name"].ToString();
                user.eMail = userData.Data.Rows[0]["eMail"].ToString();

                user.LoginAllowed = (bool) userData.Data.Rows[0]["Login_Allowed"];
                user.LastLoginDate = userData.Data.Rows[0]["LastLoginDate"].ToString();

                user.ShowGalleryMenu = (bool) userData.Data.Rows[0]["ShowGalleryMenu"];
                user.ShowCouponMenu = (bool) userData.Data.Rows[0]["ShowCouponMenu"];
                //user.ShowStatistics = (bool)userData.data.Rows[0]["ShowStatistics"];
                user.ShowNewsLetter = (bool) userData.Data.Rows[0]["ShowNewsLetter"];
                //user.StatDetail = (StatDetails) Convert.ToInt32(userData.Data.Rows[0]["StatDetails"]);

                user.ExportIpAddress = (bool) userData.Data.Rows[0]["ExportIpAddress"];

                if (updateLastLogin)
                {
                    userData.Data.Rows[0]["LastLoginDate"] = DateTime.Now; //.ToString("dd/MM/yyyy HH:MM:ss").Replace(".", ":");
                    userData.Update();
                }
            }
            else
            {
                user.LoginAllowed = false;
            }
            return user;
        }
    }
}