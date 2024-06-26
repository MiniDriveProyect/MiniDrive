using AutoMapper;
using MiniDrive.DTOs;
using MiniDrive.Models;

namespace MiniDrive.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FolderDTO, Folder>();
            CreateMap<Folder, FolderDTO>().ReverseMap();

            CreateMap<UserFileDTO, UserFile>();
            CreateMap<UserFile, UserFileDTO>().ReverseMap();
        }
    }
}