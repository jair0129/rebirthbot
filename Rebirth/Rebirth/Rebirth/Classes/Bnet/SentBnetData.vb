Public Class SentBnetData
    Private msg_Text As String
    Private msg_Chat As Boolean
    Private pkt_ID As Byte
    Private pkt_Name As String

    Public Sub New(ByVal pID As Byte)
        ID = pID
        msg_Chat = False
    End Sub

    Public Sub New(ByVal msg As String)
        msg_Text = msg
        msg_Chat = True
    End Sub

    Public Property TEXT() As String
        Get
            Return msg_Text
        End Get
        Set(ByVal value As String)
            msg_Text = value
        End Set
    End Property

    Public Property CHAT() As Boolean
        Get
            Return msg_Chat
        End Get
        Set(ByVal value As Boolean)
            msg_Chat = value
        End Set
    End Property

    Public ReadOnly Property NAME() As String
        Get
            Return pkt_Name
        End Get
    End Property

    Public Property ID() As Byte
        Get
            Return pkt_ID
        End Get
        Set(ByVal value As Byte)
            pkt_ID = value
            Select Case value

                Case &H0 : pkt_Name = "SID_NULL"
                Case &H2 : pkt_Name = "SID_STOPADV"
                Case &H4 : pkt_Name = "SID_SERVERLIST"
                Case &H5 : pkt_Name = "SID_CLIENTID"
                Case &H6 : pkt_Name = "SID_STARTVERSIONING" '
                Case &H7 : pkt_Name = "SID_REPORTVERSION" '
                Case &H8 : pkt_Name = "SID_STARTADVEX" '
                Case &H9 : pkt_Name = "SID_GETADVLISTEX" '
                Case &HA : pkt_Name = "SID_ENTERCHAT" '
                Case &HB : pkt_Name = "SID_GETCHANNELLIST" '
                Case &HC : pkt_Name = "SID_JOINCHANNEL" '
                Case &HE : pkt_Name = "SID_CHATCOMMAND" '
                Case &HF : pkt_Name = "SID_CHATEVENT" '
                Case &H10 : pkt_Name = "SID_LEAVECHAT" '
                Case &H12 : pkt_Name = "SID_LOCALEINFO" '
                Case &H13 : pkt_Name = "SID_FLOODDETECTED" '
                Case &H14 : pkt_Name = "SID_UDPPINGRESPONSE" '
                Case &H15 : pkt_Name = "SID_CHECKAD"
                Case &H16 : pkt_Name = "SID_CLICKAD"
                Case &H18 : pkt_Name = "SID_REGISTRY"
                Case &H19 : pkt_Name = "SID_MESSAGEBOX"
                Case &H1A : pkt_Name = "SID_STARTADVEX2"
                Case &H1B : pkt_Name = "SID_GAMEDATAADDRESS"
                Case &H1C : pkt_Name = "SID_STARTADVEX3"
                Case &H1D : pkt_Name = "SID_LOGONCHALLENGEEX"
                Case &H1E : pkt_Name = "SID_CLIENTID2"
                Case &H1F : pkt_Name = "SID_LEAVEGAME"
                Case &H21 : pkt_Name = "SID_DISPLAYAD"
                Case &H22 : pkt_Name = "SID_NOTIFYJOIN"
                Case &H25 : pkt_Name = "SID_PING"
                Case &H26 : pkt_Name = "SID_READUSERDATA"
                Case &H27 : pkt_Name = "SID_WRITEUSERDATA"
                Case &H28 : pkt_Name = "SID_LOGONCHALLENGE"
                Case &H29 : pkt_Name = "SID_LOGONRESPONSE"
                Case &H2A : pkt_Name = "SID_CREATEACCOUNT"
                Case &H2B : pkt_Name = "SID_SYSTEMINFO"
                Case &H2C : pkt_Name = "SID_GAMERESULT"
                Case &H2D : pkt_Name = "SID_GETICONDATA"
                Case &H2E : pkt_Name = "SID_GETLADDERDATA"
                Case &H2F : pkt_Name = "SID_FINDLADDERUSER"
                Case &H30 : pkt_Name = "SID_CDKEY"
                Case &H31 : pkt_Name = "SID_CHANGEPASSWORD"
                Case &H32 : pkt_Name = "SID_CHECKDATAFILE"
                Case &H33 : pkt_Name = "SID_GETFILETIME"
                Case &H34 : pkt_Name = "SID_QUERYREALMS"
                Case &H35 : pkt_Name = "SID_PROFILE"
                Case &H36 : pkt_Name = "SID_CDKEY2"
                Case &H3A : pkt_Name = "SID_LOGONRESPONSE2"
                Case &H3C : pkt_Name = "SID_CHECKDATAFILE2"
                Case &H3D : pkt_Name = "SID_CREATEACCOUNT2"
                Case &H3E : pkt_Name = "SID_LOGONREALMEX"
                Case &H3F : pkt_Name = "SID_STARTVERSIONING2"
                Case &H40 : pkt_Name = "SID_QUERYREALMS2"
                Case &H41 : pkt_Name = "SID_QUERYADURL"
                Case &H44 : pkt_Name = "SID_WARCRAFTGENERAL"
                Case &H45 : pkt_Name = "SID_NETGAMEPORT"
                Case &H46 : pkt_Name = "SID_NEWS_INFO"
                Case &H4A : pkt_Name = "SID_OPTIONALWORK"
                Case &H4B : pkt_Name = "SID_EXTRAWORK"
                Case &H4C : pkt_Name = "SID_REQUIREDWORK"
                Case &H4E : pkt_Name = "SID_TOURNAMENT"
                Case &H50 : pkt_Name = "SID_AUTH_INFO"
                Case &H51 : pkt_Name = "SID_AUTH_CHECK"
                Case &H52 : pkt_Name = "SID_AUTH_ACCOUNTCREATE"
                Case &H53 : pkt_Name = "SID_AUTH_ACCOUNTLOGON"
                Case &H54 : pkt_Name = "SID_AUTH_ACCOUNTLOGONPROOF"
                Case &H55 : pkt_Name = "SID_AUTH_ACCOUNTCHANGE"
                Case &H56 : pkt_Name = "SID_AUTH_ACCOUNTCHANGEPROOF"
                Case &H57 : pkt_Name = "SID_AUTH_ACCOUNTUPGRADE"
                Case &H58 : pkt_Name = "SID_AUTH_ACCOUNTUPGRADEPROOF"
                Case &H59 : pkt_Name = "SID_SETEMAIL"
                Case &H5A : pkt_Name = "SID_RESETPASSWORD"
                Case &H5B : pkt_Name = "SID_CHANGEEMAIL"
                Case &H5C : pkt_Name = "SID_SWITCHPRODUCT"
                Case &H5D : pkt_Name = "SID_REPORTCRASH"
                Case &H5E : pkt_Name = "SID_WARDEN"
                Case &H60 : pkt_Name = "SID_GAMEPLAYERSEARCH"
                Case &H65 : pkt_Name = "SID_FRIENDSLIST"
                Case &H66 : pkt_Name = "SID_FRIENDSUPDATE"
                Case &H67 : pkt_Name = "SID_FRIENDSADD"
                Case &H68 : pkt_Name = "SID_FRIENDSREMOVE"
                Case &H69 : pkt_Name = "SID_FRIENDSPOSITION"
                Case &H70 : pkt_Name = "SID_CLANFINDCANDIDATES"
                Case &H71 : pkt_Name = "SID_CLANINVITEMULTIPLE"
                Case &H72 : pkt_Name = "SID_CLANCREATIONINVITATION"
                Case &H73 : pkt_Name = "SID_CLANDISBAND"
                Case &H74 : pkt_Name = "SID_CLANMAKECHIEFTAIN"
                Case &H75 : pkt_Name = "SID_CLANINFO"
                Case &H76 : pkt_Name = "SID_CLANQUITNOTIFY"
                Case &H77 : pkt_Name = "SID_CLANINVITATION"
                Case &H78 : pkt_Name = "SID_CLANREMOVEMEMBER"
                Case &H79 : pkt_Name = "SID_CLANINVITATIONRESPONSE"
                Case &H7A : pkt_Name = "SID_CLANRANKCHANGE"
                Case &H7B : pkt_Name = "SID_CLANSETMOTD"
                Case &H7C : pkt_Name = "SID_CLANMOTD"
                Case &H7D : pkt_Name = "SID_CLANMEMBERLIST"
                Case &H7E : pkt_Name = "SID_CLANMEMBERREMOVED"
                Case &H7F : pkt_Name = "SID_CLANMEMBERSTATUSCHANGE"
                Case &H81 : pkt_Name = "SID_CLANMEMBERRANKCHANGE"
                Case &H82 : pkt_Name = "SID_CLANMEMBERINFORMATION"
                Case Else : pkt_Name = "Unknown Packet"
            End Select
        End Set
    End Property
End Class
