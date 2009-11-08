'RebirthBot
'Copyright (C) 2009 by Spencer Ragen
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met: 
'
'1.) Redistributions of source code must retain the above copyright notice, 
'this list of conditions and the following disclaimer. 
'2.) Redistributions in binary form must reproduce the above copyright notice, 
'this list of conditions and the following disclaimer in the documentation 
'and/or other materials provided with the distribution. 
'3.) The name of the author may not be used to endorse or promote products derived 
'from this software without specific prior written permission. 
'
'See LICENSE.TXT that should have accompanied this software for full terms and 
'conditions.

Imports System.Runtime.CompilerServices

Public Module Extends
    <Extension()> Public Function GetBytes(ByVal str As String) As Byte()
        Return New System.Text.ASCIIEncoding().GetBytes(str)
    End Function

    <Extension()> Public Function GetString(ByVal byts() As Byte) As String
        Return New System.Text.ASCIIEncoding().GetString(byts)
    End Function
End Module
