using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace ELFHeader
{

    public unsafe static class Program
    {

        public static void Main(string[] args)
        {
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ELF_HEADER32
    {
        public E_IDENT_HEADER e_ident;
        public ushort e_type;
        public ELFMachine e_machine;
        public ushort e_version;
        public uint* e_entry;
        public uint e_phoff;
        public uint e_shoff;
        public uint e_flags;
        public ushort e_ehsize;
        public ushort e_phentsize;
        public ushort e_phnum;
        public ushort e_shentsize;
        public ushort e_shnum;
        public ushort e_shstrndx;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ELF_HEADER64
    {
        public E_IDENT_HEADER e_ident;
        public ushort e_type;
        public ELFMachine e_machine;
        public ushort e_version;
        public ulong* e_entry;
        public ulong e_phoff;
        public ulong e_shoff;
        public uint e_flags;
        public ushort e_ehsize;
        public ushort e_phentsize;
        public ushort e_phnum;
        public ushort e_shentsize;
        public ushort e_shnum;
        public ushort e_shstrndx;
    }

    [StructLayout(LayoutKind.Sequential, Size = 16)]
    public struct E_IDENT_HEADER
    {
        fixed char ei_mag[4];




        public E_IDENT_HEADER()
        {
            ei_mag[0] = '\x7f';
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SECTION_HEADER
    {
        public uint sh_name;
        public uint sh_type;
        public uint sh_flags;
        public uint* sh_addr;
        public uint sh_offset;
        public uint sh_size;
        public uint sh_link;
        public uint sh_info;
        public uint sh_addralign;
        public uint sh_entsize;      
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sym32
    {
        public uint st_name;
        public uint* st_value;
        public uint st_size;
        public byte st_info;
        public byte st_other;
        public ushort st_shndx;    
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sym64
    {
        public uint st_name;
        public byte st_info;
        public byte st_other;
        public ushort st_shndx;
        public uint* st_value;
        public uint st_size;     
    }

    [Serializable, Flags]
    public enum ELFMachine
        : ushort
    {
        NotDefined = 0x00,
        SPARC = 0x02,
        x86 = 0x03,
        MIPS = 0x08,
        PowerPC = 0x14,
        ARM = 0x28,
        SuperH = 0x2a,
        IA_64 = 0x32,
        x86_64 = 0x3e,
        AArch64 = 0xb7,
    }
}
