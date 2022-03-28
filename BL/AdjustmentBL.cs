using DL;
using DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AdjustmentBL : IAdjustmentBL
    {
        IAdjustmentDL adjustmentDL;
        IUserDL userDL;
        ILF_DL LF_DL;
        public AdjustmentBL(IAdjustmentDL adjustmentDL, IUserDL userDL, ILF_DL LF_DL)
        {
            this.adjustmentDL = adjustmentDL;
            this.userDL = userDL;
            this.LF_DL = LF_DL;
        }
        public async Task<List<Adjustment>> getAdjustmentsByLF_Id(int LFid)
        {
            return await adjustmentDL.getAdjustmentsByLF_Id(LFid);
        }
        public async Task<int> putAdjustment(int id, Adjustment adjust)
        {
            return await adjustmentDL.putAdjustment(id, adjust);
        }
        public async Task deleteAdjustment(int id)
        {
           await adjustmentDL.deleteAdjustment(id);
        }


        public async Task<int> sendEmail(Adjustment adjustRow)
        {
            NewLF lost = await LF_DL.getLF(adjustRow.LostId);
            NewLF found = await LF_DL.getLF(adjustRow.FoundId);
            User lostUser = await userDL.getUser(lost.LF.UserId);
            User foundUser = await userDL.getUser(found.LF.UserId);

            //מייל למאבד
            MailMessage lostMessage = new MailMessage();
            lostMessage.From = new MailAddress("212325302@mby.co.il");//לשנות הרשאות בגימל של תמר
            lostMessage.To.Add(new MailAddress(lostUser.Email));
            string mailbody = "שלום " + lostUser.Name + "שמחים לעדכן אותך שיתכן שמצאנו את ה " + lost.LF.Description + "שלך. " + "\n ליצירת קשר עם המוצא: \n כתובת מייל: " + foundUser.Email;
            if (foundUser.Fhone != null)
                mailbody += "\n מספר טלפון: " + foundUser.Fhone + "\n";
            mailbody += "בכדי לייעל את פעילות האתר ולחסוך ממך קבלת הודעות נוספות על אבידה זו, אנא השב לנו אם אכן הועלנו לך  ";
            string endBody = "\n\n\n אבידה מס' " + lost.LF.Id + "\n מציאה מס' " + found.LF.Id + "\n התאמה מס' " + adjustRow.Id;
            string link = "<a href= http://localhost:4200/Home > לכניסה לאתר  </a>";

            lostMessage.Body = mailbody + link + endBody + new Attachment("M:\\logo.jpg");
            lostMessage.BodyEncoding = Encoding.UTF8;
            lostMessage.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.office365.com");    
            System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("212325302@mby.co.il", "Student@264");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;


            try
            {
                client.Send(lostMessage);
            }

            catch
            {
                return 0;
            }



            //מייל למוצא
            MailMessage foundMessage = new MailMessage();
            foundMessage.From = new MailAddress("212325302@mby.co.il");//לשנות הרשאות בגימל של תמר
            foundMessage.To.Add(new MailAddress(foundUser.Email));
            mailbody = "שלום " + foundUser.Name + "שמחים לעדכן אותך שיתכן שמצאנו את מי שאיבד את ה " + found.LF.Description + "\n ליצירת קשר עם המאבד: \n כתובת מייל: " + lostUser.Email;
            if (lostUser.Fhone != null)
                mailbody += "\n מספר טלפון: " + lostUser.Fhone + "\n";
            mailbody += "בכדי לייעל את פעילות האתר ולחסוך ממך קבלת הודעות נוספות על מציאה זו, אנא השב לנו אם אכן הועלנו לך  ";
            endBody = "\n\n\n אבידה מס' " + lost.LF.Id + "\n מציאה מס' " + found.LF.Id + "\n התאמה מס' " + adjustRow.Id;
            /*if (lost.LF.Image!=null)
                lostMessage.Attachments.Add(new Attachment(lost.LF.Image));//איך שולחים תמונה מהד"ב*/
            link = "<a href= http://localhost:4200/Home > לכניסה לאתר  </a>";

            foundMessage.Body = mailbody + link + new Attachment("M:\\logo.jpg");
            foundMessage.BodyEncoding = Encoding.UTF8;
            foundMessage.IsBodyHtml = true;
            client = new SmtpClient("smtp.office365.com"); //Gmail smtp    
            basicCredential1 = new System.Net.NetworkCredential("212325302@mby.co.il", "Student@264");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;


            try
            {
                client.Send(foundMessage);
            }

            catch
            {
                return 0;
            }
            adjustRow.StatusEmail++;
            await adjustmentDL.putAdjustment(adjustRow.Id, adjustRow);
            return 1;
        }


        //כאן נייצר פונקצית התאמה בין מציאה/אבידה לאבידות/מציאות
        //לאחר מכן נשלח לפוסט של adjustmentDL


    }


    
}



