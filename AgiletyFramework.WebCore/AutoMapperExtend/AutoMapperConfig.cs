using AgiletyFramework.DBModels.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDto;
using ModelDto;
using AgiletyFramework.IBusinessServices;
using EasyDESEncrypt;
using Microsoft.AspNetCore.Mvc;

namespace AgiletyFramework.WebCore.AutoMapperExtend
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(d => d.Id, c => c.MapFrom(u => u.Id))
                .ForMember(d => d.Name, c => c.MapFrom(u => u.Name)).AfterMap((u, d) =>
                {
                    if (u.Status == (int)StatusEnum.Frozen) d.IsEnable = false;
                    else if (u.Status == (int)StatusEnum.Normal) d.IsEnable = true;
                    else d.IsEnable = false;

                    d.Password = DESEncryptContext.DESDecrypt(u.Password);
                })
                .ReverseMap();
            CreateMap<AddUserDto, UserEntity>().AfterMap((a, u) =>
            {
                u.Roles = null;
                u.Status = (int)(a.IsEnabled ? StatusEnum.Normal : StatusEnum.Frozen);
                u.UserType = (int)UserTypeEnum.GeneralUser;

                u.CreateTime = DateTime.Now;
                u.LastLoginTime = DateTime.Now;
                u.ModifyTime = DateTime.Now;

                u.Password = DESEncryptContext.DESEncrypt(u.Password);

                u.Roles = new List<RoleEntity>();
                u.UserRoleMaps = new List<UserRoleMapEntity>();
            }).ReverseMap();

            CreateMap<MenuEntity, MenuDto>().ReverseMap();
            CreateMap<RoleEntity, RoleDto>().ReverseMap();
            CreateMap<SystemLog, LogDto>().ReverseMap();

            CreateMap<PagingData<UserEntity>, PagingData<UserDto>>().ReverseMap();
            CreateMap<PagingData<SystemLog>, PagingData<LogDto>>().ReverseMap();
        }

    }
}
