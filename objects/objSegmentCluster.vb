'    AirDrive Project: Array of IMAP repositories
'    Copyright (C) 2010 Michael Meyer
'
'    This program is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.

'    You should have received a copy of the GNU General Public License
'    along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
'    Filename: objSegmentHive.vb


Public Class objSegmentCluster
    Inherits CollectionBase

    Public Sub AddSegment(ByRef objSeg As objSegment)

    End Sub

    Public Function Add(ByVal item As objSegment)
        Return List.Add(item)
    End Function

    Public Sub Remove(ByVal item As objSegment)
        List.Remove(item)
    End Sub

    Default Public Property Item(ByVal index As Integer) As objSegment
        Get
            Return DirectCast(List(index), objSegment)
        End Get
        Set(ByVal value As objSegment)
            List(index) = value
        End Set
    End Property
End Class
