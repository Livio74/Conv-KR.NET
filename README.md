Conv-KR.NET project
====================
**If you want access to readme Italian translation click into [readme translation](README_IT.md)**

It is a conversion project from VB6 to C # of a very old project called Kripter (or KR) which is used to encrypt every single file with an encryption algorithm with Exclusive Or. Being able to encrypt every single file could have been useful with backup utilities that make the difference between files; if I decrypt the folder I modify a file and encrypt again when I make the backup only that modified file is updated.

The program, being old, has several inaccuracies and non-optimal programming methods that follow my little experience of the time. For example using non-clean code of the prefix str for all strings.

The solution, called KR.NET, is a simple conversion, probably in the future I will create a new repository where I will evolve the program to improve it from the clean code point of view (for example by eliminating the str prefixes) and adding new features to avoid for example to use symmetric cryptography which is easily decryptable.

In the [Kripter] (Kripter) folder there is the Kripter VB6 project while the other folders are the project or better the solution converted to C# where each folder is a c# .net framework project.
The VB6 project had the advantage that it was originally modularized using the modules of VB6 so the subsequent conversion was easier.

**Repository commits are in Italian so i could create a new project with English translations in the future.**

Creation of the unit test [project] (KR.NET/KRTest)
-------------------------------------------------- -

Given the modularity of the VB6 source it was easy to create using [Microsoft Visual Studio Test] (https://docs.microsoft.com/it-it/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022) a set of tests that could verify the correctness of each converted vb6 module.
Among other things, this has favored the correction of bugs so that the creation of interfaces using the module functions was limited to the creation of the controls and the conversion of only the code relating to the events and functions / subroutines internal to the forms.

Having been a manual conversion I was positively pleased to have done it before adding the forms instead of after. Maybe if you have a reliable tool (in the future I could try [VB Migration Partner] (https://www.vbmigration.com/)) I will only add tests after checking the interfaces and discovering bugs.

The launch of the tests requires first of all the creation of the test environment which is a folder containing the projects themselves in which these have been encrypted with the VB6 executable in a subfolder. This test is the First method found in [UnitTestCore.cs] (KR.NET/KRTest/UnitTestCore.cs).

Detailed considerations on conversion
---------------------------------------------

The conversion was done in a punctual way, however some parts I had or wanted to put in place , in particular below the detail.

The original project in some cases used winapi but in
[commit](../../commits/0799056a178e9a39a1c5786be6e9b37431e812b1) and in the [commit](../../commit/55a440e896551ccb431d450d65e6263d3a7105a9) I replaced them with the equivalent of the .Net framework.

In general, I fixed some bugs that I found without changing the correct function, such as in [commit](commit/6919f24654d41988616fe7c44ca119fb7fd5b189).

Towards the end of the project all the converted VB6 modules I had to bring them into a library project as there are more VBPs that use them in the VB6 project. So this was the only way to avoid having to duplicate them. The commits highlight start with ** Project Conversion KR Livio: Added Class Library with Modules **.

I added some project specific parts that were not included in the conversion process; these changes were necessary to make the KR.NET project usable without problems, in particular for the block checkbox that prevents encryption if it is not deliberately deselected, see [commit] (https://github.com/Livio74/Conv-KR.NET/commit/39d820a07a5e0dcb66f7c948b561a8f9c122bada).
I also added a check that prevents from decrypting important folders like C:\Windows etc, below are the commits:
- [commit] (../../commit/a2e7d78e0ac3ca9c018a64eecb1fb1105c9fd631)
- [commit] (../../commit/808129a9b2228072f4e99fb187163571b3449264)
- [commit] (../../commit/b3ee31f7c9a6e21b99700a56d0fc2a330d789e94)
- [commit] (../../commit/a6355845d4b8f5da4443e5307f999db7d671f808)

Instruction for using the project and launching tests
-------------------------------------------------- -----

The project was built via Visual Studio 2019, I guess it should work with later versions as well.
Therefore it is sufficient, after installing visual studio, to download the repository and open [the solution](KR.NET/KR.NET.sln) containing all the converted projects.
Once downloaded, you can rebuild it and run the tests. Launching the test creates by default a C:\KRTest folder configured by default in the [configuration file] (KR.NET/KRTest/test.runsettings) with the parameter called "WorkTestRoot" which can then be changed at will.
The launch of the tests requires first the single launch of the test method the First method present in the "UnitTestCore" test and then the launch of the whole suite to avoid that the second launch cannot be performed first and therefore causes the tests to fail.

Note
----

I have purposely chosen to include the generated executables and installation files. If they are not needed, I always have time to remove them.
