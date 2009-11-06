Module Declares

    Public BOT_VERSION As String = "1"
    Public BOT_TITLE As String = "Rebirth build " & BOT_VERSION

    Public uiBotInstance() As BotInterface
    Public ColorList() As ColorItem

    Public Const CLIENT_STAR As Long = &H53544152
    Public Const CLIENT_SEXP As Long = &H53455850
    Public Const CLIENT_W2BN As Long = &H5732424E
    Public Const CLIENT_D2DV As Long = &H44324456
    Public Const CLIENT_D2XP As Long = &H44325850
    Public Const CLIENT_WAR3 As Long = &H57415233
    Public Const CLIENT_W3XP As Long = &H57335850
    Public Const PLATID_IX86 As Long = &H49583836
    Public Const PROD_LANGUE As Long = &H656E5553
    Public Const UDPCode As Long = &H626E6574

    ' a list of all BNET text events to load
    Public BnetTextEventItems() As String = { _
                                                "accountdoesnotexist", _
                                                "accountisbanned", _
                                                "accountlogonaccepted", _
                                                "accountlogonproof", _
                                                "accountlogonsuccess", _
                                                "accountlogonsuccesssalt", _
                                                "accountupgrade", _
                                                "accountunknownerror", _
                                                "blizzardtalk", _
                                                "blizzardemote", _
                                                "broadcast", _
                                                "cdkeybanned", _
                                                "channeldoesnotexist", _
                                                "channeljoined", _
                                                "channelfull", _
                                                "channelrestricted", _
                                                "chanoptalk", _
                                                "chanopemote", _
                                                "connecting", _
                                                "connected", _
                                                "disconnected", _
                                                "enteredchat", _
                                                "error", _
                                                "exception", _
                                                "flagsupdate", _
                                                "friendsadd", _
                                                "friendslist", _
                                                "friendsremove", _
                                                "friendsposition", _
                                                "friendsupdate", _
                                                "gameversiondowngrade", _
                                                "information", _
                                                "invalidcdkey", _
                                                "invalidgameversion", _
                                                "invalidpassword", _
                                                "keyinuse", _
                                                "moderatortalk", _
                                                "moderatoremote", _
                                                "mutualfriendslist", _
                                                "mutualfriendsadd", _
                                                "mutualfriendsremove", _
                                                "mutualfriendsupdate", _
                                                "mutualfriendsposition", _
                                                "oldgameversion", _
                                                "registeremail", _
                                                "sendchatmessage", _
                                                "sendingloginrequest", _
                                                "serverclosedconnection", _
                                                "showuser", _
                                                "socketerror", _
                                                "sysoptalk", _
                                                "sysopemote", _
                                                "unhandledpacket", _
                                                "useremote", _
                                                "userjoined", _
                                                "userleft", _
                                                "usertalk", _
                                                "usinghashfile", _
                                                "usinglockdown", _
                                                "verifyinggameversion", _
                                                "versioncheckfailed", _
                                                "versioncheckpassed", _
                                                "war3serverinvalid", _
                                                "whispersent", _
                                                "whisperrecieved", _
                                                "wrongproductinfo" _
                                        }

    Public BniTextEventItems() As String = { _
                                            "count", "constructing", "extracting", "complete" _
                                        }
    Public BnftpTextEventItems() As String = { _
                                            "downloadstarted", "downloadfinished" _
                                        }

    Public FriendsTextEventParts() As String = { _
                                            "statusaway", _
                                            "statusdnd", _
                                            "statusdndaway", _
                                            "statusavailable", _
                                            "locationoffline", _
                                            "localtionnotinchat", _
                                            "locationinchat", _
                                            "locationinpublicgame", _
                                            "locationinprivategame", _
                                            "locationnonmutual", _
                                            "locationunknown" _
                                        }

End Module
