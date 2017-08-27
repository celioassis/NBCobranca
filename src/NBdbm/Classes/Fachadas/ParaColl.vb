Imports System
Imports System.Data
Imports System.Collections
Imports System.Globalization

Namespace DotNetDataProviderTemplate
    '*
    '* Because IDataParameterCollection is primarily an IList,
    '* the sample can use an existing class for most of the implementation.
    '*
    Public Class TemplateParameterCollection
        Inherits ArrayList
        Implements IDataParameterCollection

        Default Public Overloads Property Item(ByVal index As String) As Object Implements IDataParameterCollection.Item
            Get
                Return Me(IndexOf(index))
            End Get
            Set(ByVal Value As Object)
                Me(IndexOf(index)) = value
            End Set
        End Property

        Public Overloads Function Contains(ByVal parameterName As String) As Boolean Implements IDataParameterCollection.Contains
            Return (-1 <> IndexOf(parameterName))
        End Function

        Public Overloads Function IndexOf(ByVal parameterName As String) As Integer Implements IDataParameterCollection.IndexOf
            Dim index As Integer = 0
            Dim item As TemplateParameter

            For Each item In Me
                If 0 = _cultureAwareCompare(item.ParameterName, parameterName) Then
                    Return index
                End If
                index += 1
            Next
            Return -1
        End Function

        Public Overloads Sub RemoveAt(ByVal parameterName As String) Implements IDataParameterCollection.RemoveAt
            RemoveAt(IndexOf(parameterName))
        End Sub

        Public Overloads Overrides Function Add(ByVal value As Object) As Integer
            Return Add(CType(value, TemplateParameter))
        End Function

        Public Overloads Function Add(ByVal value As TemplateParameter) As Integer
            If CType(value, TemplateParameter).ParameterName <> Nothing Then
                Return MyBase.Add(value)
            Else
                Throw New ArgumentException("parameter must be named")
            End If
        End Function

        Public Overloads Function Add(ByVal parameterName As String, ByVal type As DbType) As Integer
            Return Add(New TemplateParameter(parameterName, type))
        End Function

        Public Overloads Function Add(ByVal parameterName As String, ByVal value As Object) As Integer
            Return Add(New TemplateParameter(parameterName, value))
        End Function

        Public Overloads Function Add(ByVal parameterName As String, ByVal dbType As DbType, ByVal sourceColumn As String) As Integer
            Return Add(New TemplateParameter(parameterName, dbType, sourceColumn))
        End Function

        Private Function _cultureAwareCompare(ByVal strA As String, ByVal strB As String) As Integer
            Return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreKanaType Or CompareOptions.IgnoreWidth Or CompareOptions.IgnoreCase)
        End Function
    End Class
End Namespace
