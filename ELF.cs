/* COPYRIGHT (C) 2016, Unknown6656 */

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom;
using System.Text;
using System.Linq;
using System;

namespace CoreLib.Runtime
{

    /// <summary>
    /// Manages all functions concerning ELF Headers
    /// </summary>
    public static class ELFHeader
    {
        /// <summary>
        /// The ELF magic number
        /// </summary>
        public const int EI_MAG = 0x7f454c46;
        /// <summary>
        /// The E_Ident header size
        /// </summary>
        public const int EI_NIDENT = 0x00000010;
        /// <summary>
        /// The Elf32_PHdr header size
        /// </summary>
        public const int E_PHDR32_SZ = 0x00000020;
        /// <summary>
        /// The Elf64_PHdr header size
        /// </summary>
        public const int E_PHDR64_SZ = 0x00000038;


    }

    /// <summary>
    /// An abstract ELF Header interface
    /// </summary>
    public interface IELF_HEADER
    {
        /// <summary>
        /// File interpretation header
        /// </summary>
        E_IDENT_HEADER Ident { set; get; }
        /// <summary>
        /// Object file type
        /// </summary>
        E_TYPE Type { set; get; }
        /// <summary>
        /// Target machine architecture type
        /// </summary>
        E_MACHINE Machine { set; get; }
        /// <summary>
        /// Object file version
        /// </summary>
        ushort Version { set; get; }
        /// <summary>
        /// Processor-specific machine file flags
        /// </summary>
        uint Flags { set; get; }
        /// <summary>
        /// ELF Header size
        /// </summary>
        ushort HeaderSize { set; get; }
        /// <summary>
        /// Size of a program header table entry (in bytes)
        /// </summary>
        ushort TableEntrySize { set; get; }
        /// <summary>
        /// Number of program header table entries
        /// </summary>
        ushort TableEntryCount { set; get; }
        /// <summary>
        /// Size of a section header table entry (in bytes)
        /// </summary>
        ushort SectionEntrySize { set; get; }
        /// <summary>
        /// Number of section header table entries
        /// </summary>
        ushort SectionEntryCount { set; get; }
        /// <summary>
        /// Index of the section header table entry, which contains the section name string table
        /// </summary>
        ushort SectionTableStringIndex { set; get; }
        /// <summary>
        /// Total program table header size 
        /// </summary>
        int TableHeaderSize { get; }
        /// <summary>
        /// Total section table header size 
        /// </summary>
        int SectionHeaderSize { get; }
    }

    /// <summary>
    /// Represents a 32-Bit ELF Header
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public unsafe struct ELF_HEADER32
        : IELF_HEADER
    {
        /// <summary>
        /// File interpretation header
        /// </summary>
        public E_IDENT_HEADER e_ident;
        /// <summary>
        /// Object file type
        /// </summary>
        public E_TYPE e_type;
        /// <summary>
        /// Target machine architecture type
        /// </summary>
        public E_MACHINE e_machine;
        /// <summary>
        /// Object file version
        /// </summary>
        public ushort e_version;
        /// <summary>
        /// The program file's entry point (virtual) address
        /// </summary>
        public uint* e_entry;
        /// <summary>
        /// The program file's program header table offset (int bytes)
        /// </summary>
        public uint e_phoff;
        /// <summary>
        /// The program file's section header table offset (int bytes)
        /// </summary>
        public uint e_shoff;
        /// <summary>
        /// Processor-specific machine file flags
        /// </summary>
        public uint e_flags;
        /// <summary>
        /// ELF Header size
        /// </summary>
        public ushort e_ehsize;
        /// <summary>
        /// Size of a program header table entry (in bytes)
        /// </summary>
        public ushort e_phentsize;
        /// <summary>
        /// Number of program header table entries
        /// </summary>
        public ushort e_phnum;
        /// <summary>
        /// Size of a section header table entry (in bytes)
        /// </summary>
        public ushort e_shentsize;
        /// <summary>
        /// Number of section header table entries
        /// </summary>
        public ushort e_shnum;
        /// <summary>
        /// Index of the section header table entry, which contains the section name string table
        /// </summary>
        public ushort e_shstrndx;


        /// <summary>
        /// File interpretation header
        /// </summary>
        public E_IDENT_HEADER Ident
        {
            get
            {
                return this.e_ident;
            }
            set
            {
                this.e_ident = value;
            }
        }

        /// <summary>
        /// Object file type
        /// </summary>
        public E_TYPE Type
        {
            get
            {
                return this.e_type;
            }
            set
            {
                this.e_type = value;
            }
        }

        /// <summary>
        /// Target machine architecture type
        /// </summary>
        public E_MACHINE Machine
        {
            get
            {
                return this.e_machine;
            }
            set
            {
                this.e_machine = value;
            }
        }

        /// <summary>
        /// Object file version
        /// </summary>
        public ushort Version
        {
            get
            {
                return this.e_version;
            }
            set
            {
                this.e_version = value;
            }
        }

        /// <summary>
        /// Processor-specific machine file flags
        /// </summary>
        public uint Flags
        {
            get
            {
                return this.e_flags;
            }
            set
            {
                this.e_flags = value;
            }
        }

        /// <summary>
        /// ELF Header size
        /// </summary>
        public ushort HeaderSize
        {
            get
            {
                return this.e_ehsize;
            }
            set
            {
                this.e_ehsize = value;
            }
        }

        /// <summary>
        /// Size of a program header table entry (in bytes)
        /// </summary>
        public ushort TableEntrySize
        {
            get
            {
                return this.e_phentsize;
            }
            set
            {
                this.e_phentsize = value;
            }
        }

        /// <summary>
        /// Number of program header table entries
        /// </summary>
        public ushort TableEntryCount
        {
            get
            {
                return this.e_phnum;
            }
            set
            {
                this.e_phnum = value;
            }
        }

        /// <summary>
        /// Size of a section header table entry (in bytes)
        /// </summary>
        public ushort SectionEntrySize
        {
            get
            {
                return this.e_shentsize;
            }
            set
            {
                this.e_shentsize = value;
            }
        }

        /// <summary>
        /// Number of section header table entries
        /// </summary>
        public ushort SectionEntryCount
        {
            get
            {
                return this.e_shnum;
            }
            set
            {
                this.e_shnum = value;
            }
        }

        /// <summary>
        /// Index of the section header table entry, which contains the section name string table
        /// </summary>
        public ushort SectionTableStringIndex
        {
            get
            {
                return this.e_shstrndx;
            }
            set
            {
                this.e_shstrndx = value;
            }
        }

        /// <summary>
        /// Total program table header size 
        /// </summary>
        public int TableHeaderSize
        {
            get
            {
                return this.e_phentsize * this.e_phnum;
            }
        }

        /// <summary>
        /// Total section table header size 
        /// </summary>
        public int SectionHeaderSize
        {
            get
            {
                return this.e_shentsize * this.e_shnum;
            }
        }
    }

    /// <summary>
    /// Represents a 64-Bit ELF Header
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public unsafe struct ELF_HEADER64
        : IELF_HEADER
    {
        /// <summary>
        /// File interpretation header
        /// </summary>
        public E_IDENT_HEADER e_ident;
        /// <summary>
        /// Object file type
        /// </summary>
        public E_TYPE e_type;
        /// <summary>
        /// Target machine architecture type
        /// </summary>
        public E_MACHINE e_machine;
        /// <summary>
        /// Object file version
        /// </summary>
        public ushort e_version;
        /// <summary>
        /// The program file's entry point (virtual) address
        /// </summary>
        public ulong* e_entry;
        /// <summary>
        /// The program file's program header table offset (int bytes)
        /// </summary>
        public ulong e_phoff;
        /// <summary>
        /// The program file's section header table offset (int bytes)
        /// </summary>
        public ulong e_shoff;
        /// <summary>
        /// Processor-specific machine file flags
        /// </summary>
        public uint e_flags;
        /// <summary>
        /// ELF Header size
        /// </summary>
        public ushort e_ehsize;
        /// <summary>
        /// Size of a program header table entry (in bytes)
        /// </summary>
        public ushort e_phentsize;
        /// <summary>
        /// Number of program header table entries
        /// </summary>
        public ushort e_phnum;
        /// <summary>
        /// Size of a section header table entry (in bytes)
        /// </summary>
        public ushort e_shentsize;
        /// <summary>
        /// Number of section header table entries
        /// </summary>
        public ushort e_shnum;
        /// <summary>
        /// Index of the section header table entry, which contains the section name string table
        /// </summary>
        public ushort e_shstrndx;


        /// <summary>
        /// File interpretation header
        /// </summary>
        public E_IDENT_HEADER Ident
        {
            get
            {
                return this.e_ident;
            }
            set
            {
                this.e_ident = value;
            }
        }

        /// <summary>
        /// Object file type
        /// </summary>
        public E_TYPE Type
        {
            get
            {
                return this.e_type;
            }
            set
            {
                this.e_type = value;
            }
        }

        /// <summary>
        /// Target machine architecture type
        /// </summary>
        public E_MACHINE Machine
        {
            get
            {
                return this.e_machine;
            }
            set
            {
                this.e_machine = value;
            }
        }

        /// <summary>
        /// Object file version
        /// </summary>
        public ushort Version
        {
            get
            {
                return this.e_version;
            }
            set
            {
                this.e_version = value;
            }
        }

        /// <summary>
        /// Processor-specific machine file flags
        /// </summary>
        public uint Flags
        {
            get
            {
                return this.e_flags;
            }
            set
            {
                this.e_flags = value;
            }
        }

        /// <summary>
        /// ELF Header size
        /// </summary>
        public ushort HeaderSize
        {
            get
            {
                return this.e_ehsize;
            }
            set
            {
                this.e_ehsize = value;
            }
        }

        /// <summary>
        /// Size of a program header table entry (in bytes)
        /// </summary>
        public ushort TableEntrySize
        {
            get
            {
                return this.e_phentsize;
            }
            set
            {
                this.e_phentsize = value;
            }
        }

        /// <summary>
        /// Number of program header table entries
        /// </summary>
        public ushort TableEntryCount
        {
            get
            {
                return this.e_phnum;
            }
            set
            {
                this.e_phnum = value;
            }
        }

        /// <summary>
        /// Size of a section header table entry (in bytes)
        /// </summary>
        public ushort SectionEntrySize
        {
            get
            {
                return this.e_shentsize;
            }
            set
            {
                this.e_shentsize = value;
            }
        }

        /// <summary>
        /// Number of section header table entries
        /// </summary>
        public ushort SectionEntryCount
        {
            get
            {
                return this.e_shnum;
            }
            set
            {
                this.e_shnum = value;
            }
        }

        /// <summary>
        /// Index of the section header table entry, which contains the section name string table
        /// </summary>
        public ushort SectionTableStringIndex
        {
            get
            {
                return this.e_shstrndx;
            }
            set
            {
                this.e_shstrndx = value;
            }
        }

        /// <summary>
        /// Total program table header size 
        /// </summary>
        public int TableHeaderSize
        {
            get
            {
                return this.e_phentsize * this.e_phnum;
            }
        }

        /// <summary>
        /// Total section table header size 
        /// </summary>
        public int SectionHeaderSize
        {
            get
            {
                return this.e_shentsize * this.e_shnum;
            }
        }
    }

    /// <summary>
    /// File interpretation header
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Size = ELFHeader.EI_NIDENT)]
    public unsafe struct E_IDENT_HEADER
    {
        /// <summary>
        /// The magic number 7fh 45h 4ch 46h
        /// </summary>
        [DefaultValue(ELFHeader.EI_MAG)]
        internal int ei_mag;
        /// <summary>
        /// Binary architecture
        /// </summary>
        public EI_CLASS ei_class;
        /// <summary>
        /// Binary endianess
        /// </summary>
        public EI_DATA ei_data;
        /// <summary>
        /// ELF specification version number
        /// </summary>
        public byte ei_version;
        /// <summary>
        /// Target operating system and ABI
        /// </summary>
        public EI_ABI ei_osabi;
        /// <summary>
        /// Target ABI version
        /// </summary>
        public byte ei_abiversion;
        /// <summary>
        /// &lt; reserved &gt;
        /// </summary>
        internal fixed byte __reserved__[7];

        /// <summary>
        /// Returns, whether the parent header is a 64Bit one
        /// </summary>
        public bool Is64Bit
        {
            get
            {
                return this.ei_class == EI_CLASS.CLASS64;
            }
        }

        /// <summary>
        /// The magic number 7fh 45h 4ch 46h
        /// </summary>
        public int MagicNumber
        {
            get
            {
                return ELFHeader.EI_MAG;
            }
        }

        /// <summary>
        /// Returns the current header's size
        /// </summary>
        public int Size
        {
            get
            {
                return ELFHeader.EI_NIDENT;
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Size = ELFHeader.E_PHDR32_SZ)]
    public struct ELF32_PHDR
    {
        uint p_type;
        uint p_offset;
        uint p_vaddr;
        uint p_paddr;
        uint p_filesz;
        uint p_memsz;
        uint p_flags;
        uint p_align;
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Size = ELFHeader.E_PHDR64_SZ)]
    public struct ELF64_PHDR
    {
        uint p_type;
        uint p_flags;
        ulong p_offset;
        ulong p_vaddr;
        ulong p_paddr;
        ulong p_filesz;
        ulong p_memsz;
        ulong p_align;
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
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

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sym32
    {
        public uint st_name;
        public uint* st_value;
        public uint st_size;
        public byte st_info;
        public byte st_other;
        public ushort st_shndx;    
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sym64
    {
        public uint st_name;
        public byte st_info;
        public byte st_other;
        public ushort st_shndx;
        public uint* st_value;
        public uint st_size;     
    }

    /// <summary>
    /// Target machine architecture type
    /// </summary>
    [Serializable]
    public enum E_MACHINE
        : ushort
    {
        /// <summary>
        /// An unknown machine
        /// </summary>
        NotDefined = 0x00,
        /// <summary>
        /// AT&T WE 32100
        /// </summary>
        M32 = 0x01,
        /// <summary>
        /// SPARC
        /// </summary>
        SPARC = 0x02,
        /// <summary>
        /// Intel i80386
        /// </summary>
        x86 = 0x03,
        /// <summary>
        /// Motorola 68000
        /// </summary>
        M68K = 0x04,
        /// <summary>
        /// Motorola 88000
        /// </summary>
        M88K = 0x05,
        /// <summary>
        /// Intel i80860
        /// </summary>
        x860 = 0x07,
        /// <summary>
        /// MIPS I Architecture
        /// </summary>
        MIPS = 0x08,
        /// <summary>
        /// IBM System/370 Processor
        /// </summary>
        S370 = 0x09,
        /// <summary>
        /// MIPS RS3000 Little-endian
        /// </summary>
        MIPS_RS3_LE = 0x0a,
        /// <summary>
        /// Hewlett-Packard PA-RISC
        /// </summary>
        PARISC = 0x0f,
        /// <summary>
        /// Fujitsu VPP500
        /// </summary>
        VPP500 = 0x11,
        /// <summary>
        /// Enhanced instruction set SPARC
        /// </summary>
        SPARC32Plus = 0x12,
        /// <summary>
        /// Intel i80960
        /// </summary>
        i960 = 0x013,
        /// <summary>
        /// PowerPC 32Bit
        /// </summary>
        PowerPC = 0x14,
        /// <summary>
        /// PowerPC 64Bit
        /// </summary>
        PowerPC64 = 0x15,
        /// <summary>
        /// NEC V800
        /// </summary>
        V800 = 36,
        /// <summary>
        /// Fujitsu FR20
        /// </summary>
        FR20	= 37,
        /// <summary>
        /// TRW RH-32
        /// </summary>
        RH32	= 38,
        /// <summary>
        /// Motorola RCE
        /// </summary>
        RCE = 39,
        /// <summary>
        /// Advanced RISC Machines ARM
        /// </summary>
        ARM = 0x28,
        /// <summary>
        /// Digital Alpha
        /// </summary>
        ALPHA = 0x29,
        /// <summary>
        /// Hitachi SuperH
        /// </summary>
        SuperH = 0x2a,
        /// <summary>
        /// SPARC Version 9
        /// </summary>
        SPARCv9 = 0x2b,
        /// <summary>
        /// Siemens Tricore embedded processor
        /// </summary>
        Tricore = 0x2c,
        /// <summary>
        /// Argonaut RISC Core, Argonaut Technologies Inc.
        /// </summary>
        ARC = 0x2d,
        /// <summary>
        /// Hitachi H8/300
        /// </summary>
        H8_300 = 0x2e,
        /// <summary>
        /// Hitachi H8/300H
        /// </summary>
        H8_300H = 0x2f,
        /// <summary>
        /// Hitachi H8S
        /// </summary>
        H8S = 0x30,
        /// <summary>
        /// Hitachi H8/500
        /// </summary>
        H8_500 = 0x31,
        /// <summary>
        /// Intel IA-64 processor architecture
        /// </summary>
        IA_64 = 0x32,
        /// <summary>
        /// Stanford MIPS-X
        /// </summary>
        MIPS_X = 0x33,
        /// <summary>
        /// Motorola ColdFire
        /// </summary>
        ColdFire = 0x34,
        /// <summary>
        /// Motorola M68HC12
        /// </summary>
        M68HC12 = 0x35,
        /// <summary>
        /// Fujitsu MMA Multimedia Accelerator
        /// </summary>
        MMA = 0x36,
        /// <summary>
        /// Siemens PCP
        /// </summary>
        PCP = 0x37,
        /// <summary>
        /// Sony nCPU embedded RISC processor
        /// </summary>
        NCPU = 0x38,
        /// <summary>
        /// Denso NDR1 microprocessor
        /// </summary>
        NDR1 = 0x39,
        /// <summary>
        /// Motorola Star*Core processor
        /// </summary>
        StarCore = 0x3a,
        /// <summary>
        /// Toyota ME16 processor
        /// </summary>
        ME16 = 0x3b,
        /// <summary>
        /// STMicroelectronics ST100 processor
        /// </summary>
        ST100 = 0x3c,
        /// <summary>
        /// Advanced Logic Corp. TinyJ embedded processor family
        /// </summary>
        TinyJ = 0x3d,
        /// <summary>
        /// Intel i686 architecture
        /// </summary>
        x86_64 = 0x3e,
        /// <summary>
        /// Siemens FX66 microcontroller
        /// </summary>
        FX66 = 0x41,
        /// <summary>
        /// STMicroelectronics ST9+ 8/16 bit microcontroller
        /// </summary>
        ST9Plus = 0x42,
        /// <summary>
        /// STMicroelectronics ST7 8-bit microcontroller
        /// </summary>
        ST7 = 0x43,
        /// <summary>
        /// Motorola MC68HC16 Microcontroller
        /// </summary>
        M68HC16 = 0x44,
        /// <summary>
        /// Motorola MC68HC11 Microcontroller
        /// </summary>
        M68HC11 = 0x46,
        /// <summary>
        /// Motorola MC68HC08 Microcontroller
        /// </summary>
        M68HC08 = 0x47,
        /// <summary>
        /// Motorola MC68HC05 Microcontroller
        /// </summary>
        M68HC05 = 0x48,
        /// <summary>
        /// Silicon Graphics SVx
        /// </summary>
        SVX = 0x49,
        /// <summary>
        /// STMicroelectronics ST19 8-bit microcontroller
        /// </summary>
        ST19 = 0x4a,
        /// <summary>
        /// Digital VAX
        /// </summary>
        VAX = 0x4b,
        /// <summary>
        /// Axis Communications 32-bit embedded processor
        /// </summary>
        CRIS = 0x4c,
        /// <summary>
        /// Infineon Technologies 32-bit embedded processor
        /// </summary>
        Javelin = 0x4d,
        /// <summary>
        /// Element14 64-bit DSP Processor
        /// </summary>
        Firepath = 0x4e,
        /// <summary>
        /// LSI Logic 16-bit DSP Processor
        /// </summary>
        ZSP = 0x4f,
        /// <summary>
        /// Donald Knuth's educational 64-bit processor
        /// </summary>
        MMIX = 0x50,
        /// <summary>
        /// Harvard University machine-independent object files
        /// </summary>
        HUANY = 0x51,
        /// <summary>
        /// SiTera Prism
        /// </summary>
        Prism = 0x52,
        /// <summary>
        /// AArch 64-bit architecture
        /// </summary>
        AArch64 = 0xb7,
    }

    /// <summary>
    /// Target operating system and ABI
    /// </summary>
    [Serializable, Flags]
    public enum EI_ABI
        : byte
    {
        /// <summary>
        /// UNIX System V
        /// </summary>
        SystemV = 0x00,
        /// <summary>
        /// Hewlett-Packard UNIX system
        /// </summary>
        HP_UX = 0x01,
        /// <summary>
        /// NetBSD system
        /// </summary>
        NetBSD = 0x02,
        /// <summary>
        /// Linux system
        /// </summary>
        Linux = 0x03,
        /// <summary>
        /// Solaris system
        /// </summary>
        Solaris = 0x06,
        /// <summary>
        /// IBM Advanced Interactive eXecutive UNIX system
        /// </summary>
        AIX = 0x07,
        /// <summary>
        /// Silicon Graphics IRIX system
        /// </summary>
        IRIX = 0x08,
        /// <summary>
        /// FreeBSD system
        /// </summary>
        FreeBSD = 0x09,
        /// <summary>
        /// OpenBSD system
        /// </summary>
        OpenBSD = 0x0c,
        /// <summary>
        /// OpenVMS system
        /// </summary>
        OpenVMS = 0x0d,
        /// <summary>
        /// NonStop Kernel operating system
        /// </summary>
        NSK_OS = 0x0e,
        /// <summary>
        /// AR-OS Research Operating System
        /// </summary>
        AROS = 0x0f,
        /// <summary>
        /// GNU Fenix Project
        /// </summary>
        Fenix_OS = 0x10,
        /// <summary>
        /// Cloud Application Intercafe Binary
        /// </summary>
        CloudABI = 0x11,
        /// <summary>
        /// Sortix operating system
        /// </summary>
        Sortix = 0x53,
    }

    /// <summary>
    /// Object file type
    /// </summary>
    [Serializable]
    public enum E_TYPE
        : ushort
    {
        /// <summary>
        /// Unknown
        /// </summary>
        NONE = 0,
        /// <summary>
        /// A relocatable file
        /// </summary>
        Relocatable = 1,
        /// <summary>
        /// An executable file
        /// </summary>
        Executable = 2,
        /// <summary>
        /// A shared file
        /// </summary>
        Shared = 3,
        /// <summary>
        /// A core file
        /// </summary>
        Core = 4,
    }

    /// <summary>
    /// Binary architecture type
    /// </summary>
    [Serializable, Flags]
    public enum EI_CLASS
        : byte
    {
        /// <summary>
        /// Undefined
        /// </summary>
        NONE = 0x00,
        /// <summary>
        /// 32Bit
        /// </summary>
        CLASS32 = 0x01,
        /// <summary>
        /// 64Bit
        /// </summary>
        CLASS64 = 0x02,
    }

    /// <summary>
    /// Binary endianness type
    /// </summary>
    [Serializable, Flags]
    public enum EI_DATA
        : byte
    {
        /// <summary>
        /// Undefined
        /// </summary>
        NONE = 0x00,
        /// <summary>
        /// Little endian (LSB)
        /// </summary>
        LittleEndian = 0x01,
        /// <summary>
        /// Big endian (MSB)
        /// </summary>
        BigEndian = 0x02,
    }
}
