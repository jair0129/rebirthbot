Imports System.IO

Module Functions

    Public Function GetWORD(ByVal bytByte1 As Byte, ByVal bytByte2 As Byte) As Integer
        Dim WORD As Integer
        Dim b0 As Integer
        Dim b1 As Integer

        b0 = bytByte1
        b1 = bytByte2

        WORD = (Convert.ToInt32(Convert.ToChar(b1)) << 8)
        WORD += (Convert.ToInt32(Convert.ToChar(b0)))

        Return WORD
    End Function

    Public Sub CreateColorList()
        ReDim ColorList(0)
        InsertColor(Color.AliceBlue, "AliceBlue")
        InsertColor(Color.AntiqueWhite, "AntiqueWhite")
        InsertColor(Color.Aqua, "Aqua")
        InsertColor(Color.Aquamarine, "Aquamarine")
        InsertColor(Color.Azure, "Azure")
        InsertColor(Color.Beige, "Beige")
        InsertColor(Color.Bisque, "Bisque")
        InsertColor(Color.Black, "Black")
        InsertColor(Color.BlanchedAlmond, "BlanchedAlmond")
        InsertColor(Color.Blue, "Blue")
        InsertColor(Color.BlueViolet, "BlueViolet")
        InsertColor(Color.Brown, "Brown")
        InsertColor(Color.BurlyWood, "BurlyWood")
        InsertColor(Color.CadetBlue, "CadetBlue")
        InsertColor(Color.Chartreuse, "Chartreuse")
        InsertColor(Color.Chocolate, "Chocolate")
        InsertColor(Color.Coral, "Coral")
        InsertColor(Color.CornflowerBlue, "CornflowerBlue")
        InsertColor(Color.Cornsilk, "Cornsilk")
        InsertColor(Color.Crimson, "Crimson")
        InsertColor(Color.Cyan, "Cyan")
        InsertColor(Color.DarkBlue, "DarkBlue")
        InsertColor(Color.DarkCyan, "DarkCyan")
        InsertColor(Color.DarkGoldenrod, "DarkGoldenrod")
        InsertColor(Color.DarkGray, "DarkGray")
        InsertColor(Color.DarkGreen, "DarkGreen")
        InsertColor(Color.DarkKhaki, "DarkKhaki")
        InsertColor(Color.DarkMagenta, "DarkMagenta")
        InsertColor(Color.DarkOliveGreen, "DarkOliveGreen")
        InsertColor(Color.DarkOrange, "DarkOrange")
        InsertColor(Color.DarkOrchid, "DarkOrchid")
        InsertColor(Color.DarkRed, "DarkRed")
        InsertColor(Color.DarkSalmon, "DarkSalmon")
        InsertColor(Color.DarkSeaGreen, "DarkSeaGreen")
        InsertColor(Color.DarkSlateBlue, "DarkSlateBlue")
        InsertColor(Color.DarkSlateGray, "DarkSlateGray")
        InsertColor(Color.DarkTurquoise, "DarkTurquoise")
        InsertColor(Color.DarkViolet, "DarkViolet")
        InsertColor(Color.DeepPink, "DeepPink")
        InsertColor(Color.DeepSkyBlue, "DeepSkyBlue")
        InsertColor(Color.DimGray, "DimGray")
        InsertColor(Color.DodgerBlue, "DodgerBlue")
        InsertColor(Color.Firebrick, "Firebrick")
        InsertColor(Color.FloralWhite, "FloralWhite")
        InsertColor(Color.ForestGreen, "ForestGreen")
        InsertColor(Color.Fuchsia, "Fuchsia")
        InsertColor(Color.Gainsboro, "Gainsboro")
        InsertColor(Color.GhostWhite, "GhostWhite")
        InsertColor(Color.Gold, "Gold")
        InsertColor(Color.Goldenrod, "Goldenrod")
        InsertColor(Color.Gray, "Gray")
        InsertColor(Color.Green, "Green")
        InsertColor(Color.GreenYellow, "GreenYellow")
        InsertColor(Color.Honeydew, "Honeydew")
        InsertColor(Color.HotPink, "HotPink")
        InsertColor(Color.IndianRed, "IndianRed")
        InsertColor(Color.Indigo, "Indigo")
        InsertColor(Color.Ivory, "Ivory")
        InsertColor(Color.Khaki, "Khaki")
        InsertColor(Color.Lavender, "Lavender")
        InsertColor(Color.LavenderBlush, "LavenderBlush")
        InsertColor(Color.LawnGreen, "LawnGreen")
        InsertColor(Color.LemonChiffon, "LemonChiffon")
        InsertColor(Color.LightBlue, "LightBlue")
        InsertColor(Color.LightCoral, "LightCoral")
        InsertColor(Color.LightCyan, "LightCyan")
        InsertColor(Color.LightGoldenrodYellow, "LightGoldenrodYellow")
        InsertColor(Color.LightGray, "LightGray")
        InsertColor(Color.LightGreen, "LightGreen")
        InsertColor(Color.LightPink, "LightPink")
        InsertColor(Color.LightSalmon, "LightSalmon")
        InsertColor(Color.LightSeaGreen, "LightSeaGreen")
        InsertColor(Color.LightSlateGray, "LightSlateGray")
        InsertColor(Color.LightSteelBlue, "LightSteelBlue")
        InsertColor(Color.LightYellow, "LightYellow")
        InsertColor(Color.Lime, "Lime")
        InsertColor(Color.LimeGreen, "LimeGreen")
        InsertColor(Color.Linen, "Linen")
        InsertColor(Color.Magenta, "Magenta")
        InsertColor(Color.Maroon, "Maroon")
        InsertColor(Color.MediumAquamarine, "MediumAquamarine")
        InsertColor(Color.MediumBlue, "MediumBlue")
        InsertColor(Color.MediumOrchid, "MediumOrchid")
        InsertColor(Color.MediumPurple, "MediumPurple")
        InsertColor(Color.MediumSeaGreen, "MediumSeaGreen")
        InsertColor(Color.MediumSlateBlue, "MediumSlateBlue")
        InsertColor(Color.MediumSpringGreen, "MediumSpringGreen")
        InsertColor(Color.MediumTurquoise, "MediumTurquoise")
        InsertColor(Color.MediumVioletRed, "MediumVioletRed")
        InsertColor(Color.MidnightBlue, "MidnightBlue")
        InsertColor(Color.MintCream, "MintCream")
        InsertColor(Color.MistyRose, "MistyRose")
        InsertColor(Color.Moccasin, "Moccasin")
        InsertColor(Color.NavajoWhite, "BurlyWood")
        InsertColor(Color.Navy, "BurlyWoodNavy")
        InsertColor(Color.OldLace, "OldLace")
        InsertColor(Color.Olive, "Olive")
        InsertColor(Color.OliveDrab, "OliveDrab")
        InsertColor(Color.Orange, "Orange")
        InsertColor(Color.OrangeRed, "OrangeRed")
        InsertColor(Color.Orchid, "Orchid")
        InsertColor(Color.PaleGoldenrod, "PaleGoldenrod")
        InsertColor(Color.PaleTurquoise, "PaleTurquoise")
        InsertColor(Color.PaleGreen, "PaleGreen")
        InsertColor(Color.PaleVioletRed, "PaleVioletRed")
        InsertColor(Color.PapayaWhip, "PapayaWhip")
        InsertColor(Color.Peru, "Peru")
        InsertColor(Color.Pink, "Pink")
        InsertColor(Color.Plum, "Plum")
        InsertColor(Color.PowderBlue, "PowderBlue")
        InsertColor(Color.Purple, "Purple")
        InsertColor(Color.Red, "Red")
        InsertColor(Color.RosyBrown, "RosyBrown")
        InsertColor(Color.RoyalBlue, "RoyalBlue")
        InsertColor(Color.SaddleBrown, "SaddleBrown")
        InsertColor(Color.Salmon, "Salmon")
        InsertColor(Color.SandyBrown, "SandyBrown")
        InsertColor(Color.SeaGreen, "SeaGreen")
        InsertColor(Color.SeaShell, "SeaShell")
        InsertColor(Color.Sienna, "Sienna")
        InsertColor(Color.Silver, "Silver")
        InsertColor(Color.SkyBlue, "SkyBlue")
        InsertColor(Color.SlateBlue, "SlateBlue")
        InsertColor(Color.SlateGray, "SlateGray")
        InsertColor(Color.SpringGreen, "SpringGreen")
        InsertColor(Color.Snow, "Snow")
        InsertColor(Color.SteelBlue, "SteelBlue")
        InsertColor(Color.Tan, "Tan")
        InsertColor(Color.Teal, "Teal")
        InsertColor(Color.Thistle, "Thistle")
        InsertColor(Color.Tomato, "Tomato")
        InsertColor(Color.Transparent, "Transparent")
        InsertColor(Color.Turquoise, "Turquoise")
        InsertColor(Color.Violet, "Violet")
        InsertColor(Color.Wheat, "Wheat")
        InsertColor(Color.White, "White")
        InsertColor(Color.WhiteSmoke, "WhiteSmoke")
        InsertColor(Color.Yellow, "Yellow")
        InsertColor(Color.YellowGreen, "YellowGreen")
        InsertColor(Color.LightSkyBlue, "LightSkyBlue")
    End Sub

    Private Sub InsertColor(ByVal val As Color, ByVal name As String)
        If ColorList(ColorList.Length() - 1) IsNot Nothing Then
            ReDim Preserve ColorList(0 To ColorList.Length())
        End If

        Dim k As String
        k = "000" & ColorList.Length() - 1
        k = "{%" & k.Substring(k.Length() - 3) & "}"

        ColorList(ColorList.Length() - 1) = New ColorItem(val, name, k)

        ' print out the list of colors
        'Dim bgColor As String = "000000" & Hex(RGB(val.B, val.G, val.R))
        'Dim fgColor As String = "000000" & Hex(Not RGB(val.B, val.G, val.R))
        'bgColor = "#" & Right(bgColor, 6)
        'fgColor = "#" & Right(fgColor, 6)
        '
        'Static i As Integer = 0
        'Dim writer As StreamWriter
        'writer = File.AppendText(Application.StartupPath & "\color.txt")
        'writer.WriteLine("      <td style=""background-color:" & bgColor & ";"">")
        'writer.WriteLine("          " & k)
        'writer.WriteLine("      </td>")
        'i += 1
        'If i = 11 Then
        'writer.WriteLine("  </tr>")
        'writer.WriteLine("  <tr>")
        'i = 0
        'End If
        'writer.Flush()
        'writer.Close()
    End Sub

    ' returns White on unknown code
    Public Function ParseColorParam(ByVal param As String) As ColorItem
        Dim t As String = param

        t = t.Trim()

        If t = "" Then Return ColorList(135)

        While Not IsNumeric(t.Substring(0, 1))
            'Application.DoEvents()
            If t.Length() <= 1 Then Return ColorList(135)
            t = t.Substring(1)
        End While

        While Not IsNumeric(t.Substring(t.Length() - 1, 1))
            'Application.DoEvents()
            If t.Length() <= 1 Then Return ColorList(135)
            t = t.Substring(0, t.Length() - 1)
        End While

        Dim i As Integer = Integer.Parse(t)
        If i < 0 Or i > 139 Then Return ColorList(135)

        Return ColorList(i)
    End Function

    Public Function GetChannelListImageIndex(ByVal IconList As List(Of BnetIcon), ByVal Flags As Long, ByVal Product As Long) As Integer
        If IconList Is Nothing Then Return -1
        For Each bni As BnetIcon In IconList
            If bni.UseFor(Flags, Product) Then Return bni.ImageIndex
        Next
        Return -1
    End Function

    Public Function GetProductAsLong(ByVal StatString As String) As Long
        If StatString.Length() < 4 Then Return -1

        Select Case StatString.Substring(0, 4)
            Case "RATS" : Return CLIENT_STAR
            Case "PXES" : Return CLIENT_SEXP
            Case "NB2W" : Return CLIENT_W2BN
            Case "VD2D" : Return CLIENT_D2DV
            Case "PX2D" : Return CLIENT_D2XP
            Case "3RAW" : Return CLIENT_WAR3
            Case "PX3W" : Return CLIENT_W3XP
            Case Else : Return -1
        End Select
    End Function

End Module
