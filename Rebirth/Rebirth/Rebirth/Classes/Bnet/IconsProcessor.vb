Imports System.IO
Imports System.Drawing.Imaging
Imports MBNCSUtil
Imports MBNCSUtil.Data

Public Class IconsProcessor
    Private m_file As String
    Private m_outputTemp As String
    Private m_count As Long
    Private m_icons() As BnetIcon

    Public Event IconCount(ByVal num As Integer)
    Public Event ExtractingIcons()
    Public Event ConstructingIcons()

    Public Sub New(ByVal filename As String)
        m_file = filename
    End Sub

    Private Function getRealPixelPosition(ByVal i As Integer, ByVal height As Integer, ByVal width As Integer) As Integer
        Dim x As Integer = i Mod width
        Return ((height - 1) * width) - i + (x * 2)
    End Function

    Public Function ReadIcons() As List(Of BnetIcon)
        Dim i As Integer
        Dim bniVer As Integer
        Dim input As FileStream
        Dim bytes() As Byte
        Dim m_dr As DataReader

        input = New FileStream(m_file, FileMode.Open)
        ReDim bytes(0 To CInt(input.Length - 1))
        input.Read(bytes, 0, CInt(input.Length))
        input.Close()

        m_dr = New DataReader(bytes)
        m_dr.ReadInt32()                ' header length (always 0x10)
        bniVer = m_dr.ReadInt16()       ' bni version
        m_dr.ReadInt16()                ' unused (always 0x00)
        m_count = m_dr.ReadInt32()      ' number of icons
        m_dr.ReadInt32()                ' data start

        If bniVer <> 1 Then
            Debug.Print("Unsupported BNI version")
            Throw New FormatException("Unsupported BNI version")
            Exit Function
        End If
        Debug.Print("Icons # = " & m_count)

        ReDim m_icons(0 To m_count - 1)

        RaiseEvent IconCount(m_count)
        RaiseEvent ExtractingIcons()

        For i = 0 To m_count - 1
            Application.DoEvents()
            Dim icn As New BnetIcon
            Dim products(0 To 31) As Long
            Dim numProds As Long = 0
            icn.Flags = m_dr.ReadUInt32()
            icn.Width = m_dr.ReadUInt32()
            icn.Height = m_dr.ReadUInt32()

            If icn.Flags <> 0 Then
                icn.SortIndex = i
            Else
                icn.SortIndex = m_count
            End If

            For numProds = 0 To 31
                products(numProds) = m_dr.ReadUInt32()
                If products(numProds) <> 0 Then
                    Debug.Print("icon " & i & " has product " & Hex(products(numProds)))
                End If
                If products(numProds) = 0 Then Exit For
            Next numProds

            If numProds > 0 Then
                ReDim icn.Products(0 To numProds - 1)
                Dim o As Integer
                For o = 0 To numProds - 1
                    icn.Products(o) = products(o)
                Next o
            Else
                icn.Products = Nothing
            End If

            m_icons(i) = icn
        Next i

        Dim infoLen As Byte = m_dr.ReadByte()
        m_dr.ReadByte()                             ' skip ColorMapType
        Dim imageType As Byte = m_dr.ReadByte()     ' should be 0x0A
        m_dr.ReadByteArray(5)                       ' skip ColorMapSpecification
        m_dr.ReadUInt16()                           ' skip x origin
        m_dr.ReadUInt16()                           ' skip y origin
        Dim width = m_dr.ReadUInt16()
        Dim height = m_dr.ReadUInt16()
        Dim depth As Byte = m_dr.ReadByte()
        m_dr.ReadByte()                             ' skip descriptor
        m_dr.ReadByteArray(infoLen)                 ' skip info string

        If imageType <> &HA Then
            Throw New FormatException("Unsupported image type")
            Exit Function
        End If

        If depth <> 24 Then
            Throw New FormatException("Unsupported color depth")
            Exit Function
        End If

        Dim pixelData(0 To width * height) As Integer
        Dim currentPixel As Integer = 0

        While currentPixel < pixelData.Length()
            Application.DoEvents()
            Dim packetHeader As Byte = m_dr.ReadByte()
            Dim len As Integer = (packetHeader And &H7F) + 1

            If (packetHeader And &H80) <> 0 Then
                ' bit 7 is 1, all pixels same color
                Dim blue As Byte = m_dr.ReadByte() And &HFF
                Dim green As Byte = m_dr.ReadByte() And &HFF
                Dim red As Byte = m_dr.ReadByte() And &HFF

                Dim col As Integer = RGB(blue, green, red)

                For i = 0 To len - 1
                    Dim pixelPos As Integer = getRealPixelPosition(currentPixel, height, width)

                    If pixelPos > pixelData.Length() Then Exit For
                    If pixelPos >= 0 Then
                        pixelData(pixelPos) = col
                    End If
                    currentPixel += 1
                Next i
            Else
                ' each pixel has its own color
                For i = 0 To len - 1
                    Dim blue As Byte = m_dr.ReadByte() And &HFF
                    Dim green As Byte = m_dr.ReadByte() And &HFF
                    Dim red As Byte = m_dr.ReadByte() And &HFF

                    Dim col As Integer = RGB(blue, green, red)
                    Dim pixelPos As Integer = getRealPixelPosition(currentPixel, height, width)

                    If pixelPos > pixelData.Length() Then Exit For
                    If pixelPos >= 0 Then
                        pixelData(pixelPos) = col
                    End If
                    currentPixel += 1
                Next i
            End If
        End While

        currentPixel = 0

        RaiseEvent ConstructingIcons()
        Dim ret As New List(Of BnetIcon)

        For Each bni As BnetIcon In m_icons
            Application.DoEvents()
            Dim iconPath As String = Application.StartupPath & "\icons\" & Hex(bni.Flags) & ".bmp"

            bni.p_Image = New Bitmap(bni.Width, bni.Height, PixelFormat.Format32bppRgb)

            Using fp As New FastPix(bni.p_Image)
                Try
                    Dim y As Integer = 0
                    Dim x As Integer = 0
                    Dim pos As Integer = 0

                    For y = 0 To bni.Height - 1
                        For x = 0 To bni.Width - 1
                            fp.SetPixel(x, y, Color.FromArgb(pixelData(currentPixel + pos)))
                            pos += 1
                        Next x
                    Next y
                    currentPixel += pos
                Catch ex As Exception
                    currentPixel += bni.Width * bni.Height
                End Try
            End Using
            FastPix.ConvertFormat(bni.p_Image, PixelFormat.Format24bppRgb)
            ret.Add(bni)
        Next bni

        Return ret
    End Function


End Class
