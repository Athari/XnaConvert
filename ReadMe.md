Alba.XnaConvert
===============

Command-line utility for converting XNA XNB files.

Features
--------

* Converting Texture2D XNB files to PNG images.
* All versions of XNA are supported: 1.0, 2.0, 3.0, 3.1, 4.0.

N.B.
----

Do not forget: the fact that you *can technically extract* and view the resources from some games does not mean you *can legally use* the resources. Even extracting can be illegal in your country. If you are unsure, do not use this program.

Installation
------------

You need to install the following redistributable packages before using the program (also necessary for compiling from sources):

* [Microsoft .NET Framework 4.5](http://www.microsoft.com/en-us/download/details.aspx?id=30653)
* [Microsoft XNA Framework Redistributable 1.0](http://www.microsoft.com/en-us/download/details.aspx?id=2431)
* [Microsoft XNA Framework Redistributable 2.0](http://www.microsoft.com/en-us/download/details.aspx?id=15537)
* [Microsoft XNA Framework Redistributable 3.0](http://www.microsoft.com/en-us/download/details.aspx?id=22588)
* [Microsoft XNA Framework Redistributable 3.1](http://www.microsoft.com/en-us/download/details.aspx?id=15163)
* [Microsoft XNA Framework Redistributable 4.0](http://www.microsoft.com/en-us/download/details.aspx?id=20914)

Command line
------------

**Verb: convert**

Convert file from one format to another.

Options:

* `-l, --library` *(Default: XNA)* Library name.
* `-v, --version` *(Default: 4.0)* Library version.
* `-i, --input` *(Required)* Input file (*.xnb).
* `-d, --inputdir` *(Required)* Input directory (with *.xnb files).
* `-m, --mask` *(Default: *.xnb)* Input mask (with *.xnb files).
* `-r, --recursive` *(Default: false)* Process files in input directory recursively.
* `-o, --output` *(Required)* Output file or directory.

Examples:

    Alba.XnaConvert convert -v 4 -d "C:\Games\Terraria\Content\Images" -o "C:\Unpacked\Terraria"
    Alba.XnaConvert convert -v 4 -r -d "C:\Games\Dust An Elysian Tail\content\gfx" -o "C:\Unpacked\Dust An Elysian Tail"
    Alba.XnaConvert convert -v 3.1 -r -d "C:\Games\Capsized\Content" -o "C:\Unpacked\Capsized"
    Alba.XnaConvert convert -v 3 -d "C:\Games\Blueberry Garden\Content" -o "C:\Unpacked\Blueberry Garden"

**Verb: listlibs**

List supported libraries and versions.

Options:

* `-a, --all` *(Default: false)* Include all aliases.

Examples:

    Alba.XnaConvert listlibs
    Alba.XnaConvert listlibs --all

Known issues
------------

1. XNA sometimes fails to properly free resources. If multiple huge images are converted and your computer is low on memory, the program will terminate. *Workaround:* run the program again. It will skip already converted files.

License
-------

**New BSD**

Copyright © 2013, Athari
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of the Athari nor the names of other contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL ATHARI BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
