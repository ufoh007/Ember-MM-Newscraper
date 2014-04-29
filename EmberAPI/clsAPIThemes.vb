﻿' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports System.IO
Imports EmberAPI

Public Class Theme

#Region "Fields"

    Private _title As String
    Private _id As String
    Private _url As String
    Private _description As String
    Private _length As String
    Private _bitrate As String

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Public Property URL() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property Length() As String
        Get
            Return _length
        End Get
        Set(ByVal value As String)
            _length = value
        End Set
    End Property

    Public Property Bitrate() As String
        Get
            Return _bitrate
        End Get
        Set(ByVal value As String)
            _bitrate = value
        End Set
    End Property

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        _title = String.Empty
        _id = String.Empty
        _url = String.Empty
        _description = String.Empty
        _length = String.Empty
        _bitrate = String.Empty
    End Sub

    Public Sub Cancel()
        'Me.WebPage.Cancel()
    End Sub

    ''' <summary>
    ''' Remove existing trailers from the given path.
    ''' </summary>
    ''' <param name="sPath">Path to look for trailers</param>
    ''' <param name="NewTheme"></param>
    ''' <remarks>
    ''' 2013/11/08 Dekker500 - Enclosed file accessors in Try block
    ''' </remarks>
    Public Shared Sub DeleteThemes(ByVal sPath As String, ByVal NewTheme As String)

    End Sub
    ''' <summary>
    ''' Downloads the theme found at the supplied <paramref name="sURL"/> and places
    ''' it in the supplied <paramref name="sPath"/>. 
    ''' </summary>
    ''' <param name="sPath">Path into which the theme should be saved</param>
    ''' <param name="sURL">URL from which to get the theme</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DownloadTheme(ByVal sPath As String, ByVal isSingle As Boolean, ByVal sURL As String) As String
        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim lhttp As New HTTP
        Dim tTheme As String = String.Empty
        'AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        tTheme = lhttp.DownloadFile(sURL, Path.Combine(Master.TempPath, "theme"), False, "theme")
        Dim fExt As String = Path.GetExtension(tTheme)
        For Each a In FileUtils.GetFilenameList.Movie(sPath, isSingle, Enums.ModType.Theme)
            If File.Exists(a & fExt) Then
                File.Delete(a & fExt)
            End If
            File.Copy(tTheme, a & fExt)
            tURL = a & fExt
        Next
        File.Delete(tTheme)
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Return tURL
    End Function
    ''' <summary>
    ''' Determines whether a theme is allowed to be downloaded. This is determined
    ''' by a combination of the Master.eSettings.LockTheme settings,
    ''' whether the path is valid, and whether the Master.eSettings.OverwriteTheme
    ''' flag is set. 
    ''' </summary>
    ''' <param name="sPath">The intended path to save the theme</param>
    ''' <param name="isDL">Flag to indicate whether the file is intended to be saved to the file system or not</param>
    ''' <param name="isSS">Flag to indicate whether a scrape of a single item was requested (Enums.ScrapeType.SingleScrape), or whether this is part of a multi-item scrape</param>
    ''' <returns><c>True</c> if a download is allowed, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function IsAllowedToDownload(ByVal sPath As String, ByVal isDL As Boolean, Optional ByVal isSS As Boolean = False) As Boolean
        'TODO Dekker500 - MUST VALIDATE whether these parameters are correct! I believe isDL and isSS are reversed in meanings (at least from the calling method's perspective)!!!!

        'Dim fScanner As New Scanner
        If Master.eSettings.MovieThemeOverwrite Then
            Return True
        Else
            Return False
        End If

        'If isDL Then
        '    'If String.IsNullOrEmpty(fScanner.GetMovieTrailerPath(sPath)) OrElse Master.eSettings.MovieThemeOverwrite Then
        '    If Master.eSettings.MovieThemeOverwrite Then
        '        Return True
        '    Else
        '        If isSS AndAlso String.IsNullOrEmpty(fScanner.GetMovieTrailerPath(sPath)) Then
        '            If Not Master.eSettings.MovieLockTheme Then
        '                Return True
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If
        'Else
        '    If Not Master.eSettings.MovieLockTheme Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End If
    End Function
    ''' <summary>
    ''' Raises the ProgressUpdated event, passing the iPercent value to indicate percent completed.
    ''' </summary>
    ''' <param name="iPercent">Integer representing percentage completed</param>
    ''' <remarks></remarks>
    Public Shared Sub DownloadProgressUpdated(ByVal iPercent As Integer)
        RaiseEvent ProgressUpdated(iPercent)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class