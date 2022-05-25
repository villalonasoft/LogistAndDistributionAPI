using AutoMapper;
using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.AutoMapperProfiles
{
    public class CedisPickingProfile : Profile
    {
        public CedisPickingProfile()
        {
            ProfileDomain();
        }

        private void ProfileDomain()
        {
            CreateMap<OrderHeader, BranchOrderHeaderViewDto>()
                .ForMember(m => m.Status, prop => prop.MapFrom(c => c.Status.Name))
                .ForMember(m => m.Details, prop => prop.MapFrom(c => c.OrderDetails))
                .ForMember(m => m.BackgroudColor, prop => prop.MapFrom(c => c.Status.BackgroudColor))
                .ForMember(m => m.BranchName, prop => prop.MapFrom(c => c.Branch.Name))
                .ForMember(m => m.Zones, prop => prop.MapFrom(c => c.Zones.Name))
                .ForMember(m => m.Mode, prop => prop.MapFrom(c => c.Mode.Name))
                .ForMember(m => m.User, prop => prop.MapFrom(c => c.UserId.ToString() + " PENDIENTE"))
                .ForMember(m => m.DateInit, prop => prop.MapFrom(c => c.DateInit.ToString("dd/MM/yyyy")))
                .ForMember(m => m.DateEnd, prop => prop.MapFrom(c => c.DateEnd.ToString("dd/MM/yyyy")));

            CreateMap<BranchOrder, BranchOrderViewDto>()
                .ForMember(m => m.Status, prop => prop.MapFrom(c => c.Status.Name))
                .ForMember(m => m.Detail, prop => prop.MapFrom(c => c.BranchOrderDetails))
                .ForMember(m => m.BackgroudColor, prop => prop.MapFrom(c => c.Status.BackgroudColor))
                .ForMember(m => m.BranchName, prop => prop.MapFrom(c => c.Branch.Name))
                .ForMember(m => m.Warehouse, prop => prop.MapFrom(c => c.Warehouse.Name))
                .ForMember(m => m.Date, prop => prop.MapFrom(c => c.Date.ToString("dd/MM/yyyy")))
                .AfterMap((src,dest)=> {
                    switch (DateTime.Now.DayOfWeek.ToString().ToUpper().Trim())
                    {
                        case "MONDAY":
                            dest.CanOrder = src.Branch.Schedule.Monday;
                            break;
                        case "TUESDAY":
                            dest.CanOrder = src.Branch.Schedule.Tuesday;
                            break;
                        case "WEDNESDAY":
                            dest.CanOrder = src.Branch.Schedule.Wednesday;
                            break;
                        case "THURSDAY":
                            dest.CanOrder = src.Branch.Schedule.Thursday;
                            break;
                        case "FRIDAY":
                            dest.CanOrder = src.Branch.Schedule.Friday;
                            break;
                        case "SATURDAY":
                            dest.CanOrder = src.Branch.Schedule.Saturday;
                            break;
                        case "SUNDAY":
                            dest.CanOrder = src.Branch.Schedule.Sunday;
                            break;
                    }
                });

            CreateMap<BranchOrderCreate, BranchOrder>()
                .ForMember(m => m.Reference, prop => prop.MapFrom(c => c.BranchReference.ToString()))
                .ForMember(m => m.BranchOrderDetails, prop => prop.MapFrom(c => c.Details));

            CreateMap<BranchOrderDetailCreate, BranchOrderDetail>();

            CreateMap<BranchOrderDetail, BranchOrderDetailViewDto>()
                .ForMember(m => m.ProductName, pro => pro.MapFrom(c => c.PresentationWarehouse.Presentation.Name))
                .ForMember(m => m.Zone, pro => pro.MapFrom(c => c.PresentationWarehouse.Zones.Name))
                .ForMember(m => m.Unit, pro => pro.MapFrom(c => c.Unit.Description))
                .ForMember(m => m.Pasillo, pro => pro.MapFrom(c => c.PresentationWarehouse.Pasillo))
                .ForMember(m => m.Tramo, pro => pro.MapFrom(c => c.PresentationWarehouse.Tramo))
                .ForMember(m => m.Bandeja, pro => pro.MapFrom(c => c.PresentationWarehouse.Bandeja))
                .ForMember(m => m.Ubitramo, pro => pro.MapFrom(c => c.PresentationWarehouse.Ubitramo));


            CreateMap<OrderDetail, BranchOrderHeaderDetailDto>()
                .ForMember(m => m.ProductName, pro => pro.MapFrom(c => c.PresentationWarehouse.Presentation.Name))
                .ForMember(m => m.Status, pro => pro.MapFrom(c => c.Status.Name))
                .ForMember(m => m.Pasillo, pro => pro.MapFrom(c => c.PresentationWarehouse.Pasillo))
                .ForMember(m => m.Tramo, pro => pro.MapFrom(c => c.PresentationWarehouse.Tramo))
                .ForMember(m => m.Ubitramo, pro => pro.MapFrom(c => c.PresentationWarehouse.Ubitramo))
                .ForMember(m => m.Zone, pro => pro.MapFrom(c => c.PresentationWarehouse.Zones.Name))
                .ForMember(m => m.Units, pro => pro.MapFrom(c => c.Units.Description))
                .ForMember(m => m.Bandeja, pro => pro.MapFrom(c => c.PresentationWarehouse.Bandeja));

            #region USER
            CreateMap<User, UserViewDto>();
            CreateMap<UserCreateDto, User>()
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(x=>x.UserName.ToUpper().Trim()));
            #endregion

            #region BRANCHES
            CreateMap<Branch, BranchViewDto>()
                .ForMember(x=>x.Monday, opt=>opt.MapFrom(y=>y.Schedule.Monday))
                .ForMember(x=>x.Tuesday, opt=>opt.MapFrom(y=>y.Schedule.Tuesday))
                .ForMember(x=>x.Wednesday, opt=>opt.MapFrom(y=>y.Schedule.Wednesday))
                .ForMember(x=>x.Thursday, opt=>opt.MapFrom(y=>y.Schedule.Thursday))
                .ForMember(x=>x.Friday, opt=>opt.MapFrom(y=>y.Schedule.Friday))
                .ForMember(x=>x.Saturday, opt=>opt.MapFrom(y=>y.Schedule.Saturday))
                .ForMember(x=>x.Sunday, opt=>opt.MapFrom(y=>y.Schedule.Sunday));

            CreateMap<BranchViewDto, Branch>()
                .AfterMap((src, dest) => {
                    dest.Schedule = new Schedule()
                    {
                        Saturday=src.Saturday,
                        Sunday = src.Sunday,
                        Monday = src.Monday,
                        Friday = src.Friday,
                        Thursday = src.Thursday,
                        Tuesday = src.Tuesday,
                        Wednesday = src.Wednesday
                    };
                });
            #endregion
        }
    }
}
