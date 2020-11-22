using System;
using System.Globalization;
using Sede_Checker.DTO;
using Sede_Checker.Entities;
using Sede_Checker.Entities.DTO;
using Sede_Checker.TaskFormParams.Properties;

namespace Sede_Checker.Entities.Converters
{
    internal sealed class SedeTaskConverter : SedeBaseEntityDtoConverter<SedeTaskData, SedeTaskDataDto>
    {
        public override SedeTaskDataDto ConvertToDtoEntity(SedeTaskData uiEntity)
        {
            var r = new SedeTaskDataDto
            {
                //Task identificator: if not exist -> create new GUID
                TaskGuid = uiEntity.TaskGuid,
                Province = uiEntity.ProcedureRegion,
                ProcedureForSelectedProvince = uiEntity.ProcedureName,
                NumberToInsert = uiEntity.CustomerPhoneNumber,
                DateCardEnd = uiEntity.TieExpiredDate,
                Passport = uiEntity.DocumentNumber,
                NameSurname = uiEntity.CustomerNameAndSurname,
                CustomerDateOfBirth = uiEntity.CustomerDateOfBirth,
                Country = uiEntity.Country,
                NeadableCitaTown = uiEntity.ProcedureCity,
                MailToNotifications = uiEntity.CustomerEmail,
                ReasonToGetNIE = uiEntity.ProcedureReason,
                TaskCreatedDateTime = uiEntity.TaskCreatedDateTime,
                SitaReceived = uiEntity.IsCitaReceived,
                TaskStatus = uiEntity.TaskStatus,
                CitaIdentificationNumber = uiEntity.CitaIdentificationNumber,
                CitaDateTime = uiEntity.CitaDateTime,
                CitaNotes = uiEntity.CitaNotes,
                SitaMinRequiredDate = uiEntity.SitaMinRequiredDate,
                SitaMaxRequiredDate = uiEntity.SitaMaxRequiredDate,
                FamiliarConnectionWithCitizen = uiEntity.FamiliarConnectionWithCitizen,
                FamiliarDocumentNumber = uiEntity.FamiliarDocumentNumber,
                FamiliarNameAndSurname = uiEntity.FamiliarNameAndSurname,
                FamiliarNationality = uiEntity.FamiliarNationality
            };

            //todo: Why is so strange values?
            switch (uiEntity.DocumentType)
            {
                case DocumentType.PASSPORT:
                    r.DocumentTypeId = "rdbTipoDocPas";
                    break;
                case DocumentType.NIE:
                    r.DocumentTypeId = "rdbTipoDocNie";
                    break;
                case DocumentType.DNI:
                    r.DocumentTypeId = "rdbTipoDocDni";
                    break;
                case DocumentType.INCIAL:
                    r.DocumentTypeId = "rdTipoSolicitudI";
                    break;
                case DocumentType.RENOVACION:
                    r.DocumentTypeId = "rdTipoSolicitudR";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (uiEntity.ProcedureTramite)
            {
                case BarcelonaTramites.AutorizacionDeRegreso:
                    r.ProceduresByPersonalCode = BarcelonaTramitesDto.AutorizacionDeRegreso;
                    break;
                case BarcelonaTramites.CertificatesUe:
                    r.ProceduresByPersonalCode = BarcelonaTramitesDto.CertificatesUe;
                    break;
                case BarcelonaTramites.ExpeditionDeTarjetaDeIdentitadDeExtranjero:
                    r.ProceduresByPersonalCode = BarcelonaTramitesDto.ExpeditionDeTarjetaDeIdentitadDeExtranjero;
                    break;
                case BarcelonaTramites.CertificadosYAsicnacionNie:
                    r.ProceduresByPersonalCode = BarcelonaTramitesDto.CertificadosYAsicnacionNie;
                    break;
                case BarcelonaTramites.ExpedicionDeDocumentosDeSoliciantesDeAsilo:
                    r.ProceduresByPersonalCode = BarcelonaTramitesDto.ExpedicionDeDocumentosDeSoliciantesDeAsilo;
                    break;
                default:
                    r.ProceduresByPersonalCode = null;
                    break;
            }

            return r;
        }

        public override SedeTaskData ConvertToUiEntity(SedeTaskDataDto dtoEntity)
        {
            var r = new SedeTaskData
            {  
                TaskGuid = dtoEntity.TaskGuid,
                ProcedureRegion = dtoEntity.Province,
                ProcedureTramite = dtoEntity.ProceduresByPersonalCode,
                ProcedureName = dtoEntity.ProcedureForSelectedProvince,
                CustomerPhoneNumber = dtoEntity.NumberToInsert,
                TieExpiredDate = dtoEntity.DateCardEnd, 
                DocumentNumber = dtoEntity.Passport,
                CustomerNameAndSurname = dtoEntity.NameSurname,
                Country = dtoEntity.Country,
                ProcedureCity = dtoEntity.NeadableCitaTown,
                CustomerEmail = dtoEntity.MailToNotifications,
                ProcedureReason = dtoEntity.ReasonToGetNIE,
                TaskCreatedDateTime = dtoEntity.TaskCreatedDateTime,
                CustomerDateOfBirth = dtoEntity.CustomerDateOfBirth,
                TaskStatus = dtoEntity.TaskStatus,
                CitaIdentificationNumber = dtoEntity.CitaIdentificationNumber,
                CitaDateTime = dtoEntity.CitaDateTime,
                CitaNotes = dtoEntity.CitaNotes,
                SitaMinRequiredDate = dtoEntity.SitaMinRequiredDate,
                SitaMaxRequiredDate = dtoEntity.SitaMaxRequiredDate,
                FamiliarConnectionWithCitizen = dtoEntity.FamiliarConnectionWithCitizen,
                FamiliarNationality = dtoEntity.FamiliarNationality,
                FamiliarNameAndSurname = dtoEntity.FamiliarNameAndSurname,
                FamiliarDocumentNumber = dtoEntity.FamiliarDocumentNumber
            };

            //todo: Why is so strange values?
            switch (dtoEntity.DocumentTypeId)
            {
                case "rdbTipoDocNie":
                    r.DocumentType = DocumentType.NIE;
                    break;
                case "rdbTipoDocPas":
                    r.DocumentType = DocumentType.PASSPORT;
                    break;
                case "rdbTipoDocDni":
                    r.DocumentType = DocumentType.DNI;
                    break;
                case "rdTipoSolicitudI":
                    r.DocumentType = DocumentType.INCIAL;
                    break;
                case "rdTipoSolicitudR":
                    r.DocumentType = DocumentType.RENOVACION;
                    break;
                default:
                    r.DocumentType = DocumentType.PASSPORT;
                    break;
            }

            switch (r.ProcedureTramite)
            {
                case BarcelonaTramitesDto.AutorizacionDeRegreso:
                    r.ProcedureTramite = BarcelonaTramites.AutorizacionDeRegreso;
                    break;
                case BarcelonaTramitesDto.CertificatesUe:
                    r.ProcedureTramite = BarcelonaTramites.CertificatesUe;
                    break;
                case BarcelonaTramitesDto.ExpeditionDeTarjetaDeIdentitadDeExtranjero:
                    r.ProcedureTramite = BarcelonaTramites.ExpeditionDeTarjetaDeIdentitadDeExtranjero;
                    break;
                case BarcelonaTramitesDto.CertificadosYAsicnacionNie:
                    r.ProcedureTramite = BarcelonaTramites.CertificadosYAsicnacionNie;
                    break;
                case BarcelonaTramitesDto.ExpedicionDeDocumentosDeSoliciantesDeAsilo:
                    r.ProcedureTramite = BarcelonaTramites.ExpedicionDeDocumentosDeSoliciantesDeAsilo;
                    break;
                default:
                    r.ProcedureTramite = null;
                    break;
            }

            return r;
        }
    }
}