using Sede_Checker.DTO;

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sede_Checker.Entities.DTO;

namespace Sede_Checker.TaskFormParams.converters
{
    class TableConverter
    {
        public List<SedeCheckerUserDto> Convert(List<SedeCheckerUserDto> UsersList)
        {
            try
            {
                foreach(var u in UsersList)
                {
                    switch (u.DocumentTypeId)
                    {
                        case "rdbTipoDocPas":
                            u.DocumentTypeId = "PASSPORT";
                            break;
                        case "rdbTipoDocNie":
                            u.DocumentTypeId = "N.I.E";
                            break;
                        case "rdbTipoDocDni":
                            u.DocumentTypeId = "D.N.I";
                            break;
                        default:
                            u.DocumentTypeId = "UNDEFINED";
                            break;
                    }
                    switch (u.SitaReceived)
                    {
                        case "true":
                            u.SitaReceived = "Yes";
                            break;
                        case "false":
                            u.SitaReceived = "No";
                            break;
                    }
                    if(u.ReasonToGetNIE == "null")
                    {
                        u.ReasonToGetNIE = "-";
                    }

                }
                return UsersList;
            }catch(Exception e)
            {
                MessageBox.Show($"Converter error, cannot convert users list: {e.Message}");
                return UsersList;
            }
        }

        public List<SedeCheckerUserDto> Unconvert(List<SedeCheckerUserDto> UsersList)
        {
            try
            {
                foreach (var u in UsersList)
                {
                    switch (u.DocumentTypeId)
                    {
                        case "PASSPORT":
                            u.DocumentTypeId = "rdbTipoDocPas";
                            break;
                        case "N.I.E":
                            u.DocumentTypeId = "rdbTipoDocNie";
                            break;
                        case "D.N.I":
                            u.DocumentTypeId = "rdbTipoDocDni";
                            break;
                        default:
                            u.DocumentTypeId = "UNDEFINED";
                            break;
                    }
                    switch (u.SitaReceived)
                    {
                        case "Yes":
                            u.SitaReceived = "true";
                            break;
                        case "No":
                            u.SitaReceived = "false";
                            break;
                    }
                    if (u.ReasonToGetNIE == "-")
                    {
                        u.ReasonToGetNIE = "null";
                    }

                }
                return UsersList;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Converter error, cannot unconvert users list: {e.Message}");
                return UsersList;
            }
        }
    }
}
