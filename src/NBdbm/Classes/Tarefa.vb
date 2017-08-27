Option Explicit On

Imports System.Collections

Friend Class Tarefa

    Private vIndex As Integer
    Private vStatus As String
    Private vProgress As Byte
    Private vDescricao As String
    Private vBuffer As String
    Private vObjectStatus As Object   'Deve ser compatível com o Label ou caixa de texto
    Private vObjectProgress As Object 'Deve ser compatível com o Painel ou progressBar

    Public Property Index() As Integer
        Get
            Return vIndex
        End Get
        Set(ByVal Value As Integer)
            vIndex = Value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return vStatus
        End Get
        Set(ByVal Value As String)
            vBuffer = vBuffer & vbCrLf & vStatus
            vStatus = Value
            If Not vObjectStatus Is Nothing Then
                vObjectStatus.text = Trim(vStatus)
                vObjectStatus.Refresh()
            End If
        End Set
    End Property

    Public Property progress() As Byte
        Get
            Return vProgress
        End Get
        Set(ByVal Value As Byte)
            vProgress = Value
            If Not vObjectProgress Is Nothing Then
                If vProgress > 100 Then vProgress = 100
                vObjectProgress.FloodPercent = vProgress
                vObjectProgress.Caption = Trim(vProgress & " %")
                vObjectProgress.Refresh()
            End If
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return vDescricao
        End Get
        Set(ByVal Value As String)
            vDescricao = Value
        End Set
    End Property

    Public ReadOnly Property Buffer(ByVal vNewValue As String) As Object
        Get
            Buffer = vBuffer
            vBuffer = ""
        End Get
    End Property

    Public Property objectProgress() As Object
        Get
            Return vObjectProgress
        End Get
        Set(ByVal objProgress As Object)
            vObjectProgress = objProgress
        End Set
    End Property

    Public Property objectStatus() As Object
        Get
            Return vObjectStatus
        End Get
        Set(ByVal objStatus As Object)
            vObjectStatus = objStatus
        End Set
    End Property

End Class

Friend Class Tarefas

    Implements IDisposable, IEnumerable

    'local variable to hold collection
    Private CollDesc As Collection
    Private CollIndex As Collection
    '
    Public Sub New()
        CollDesc = New Collection
        CollIndex = New Collection
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        CollDesc = Nothing
        CollIndex = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Call Me.Dispose()
    End Sub

    Public Function Add(ByVal InicialStatus As String, ByVal InicialProgress As Double, ByVal Descricao As String) As Tarefa
        Me.Add(InicialStatus, InicialProgress, Descricao, System.String.Empty, System.String.Empty)
        Return Nothing
    End Function

    Public Function Add(ByVal InicialStatus As String, ByVal InicialProgress As Double, ByVal Descricao As String, ByVal objProgress As Object, ByVal objStatus As Object) As Tarefa

        'On Error GoTo errAdd
        'create a new object

        Dim objNewMember As Tarefa
        objNewMember = New Tarefa

        'set the properties passed into the method
        If Not objProgress Is Nothing Then
            objNewMember.objectProgress = objProgress
        End If

        If Not objStatus Is Nothing Then
            objNewMember.objectStatus = objStatus
        End If

        objNewMember.Index = CollDesc.Count + 1
        objNewMember.Status = InicialStatus
        objNewMember.progress = InicialProgress
        objNewMember.Descricao = Descricao

        'set the properties passed into the method
        '    If Len(NomeCampo) = 0 Then
        '        Collection.Add objNewMember
        '    Else
        CollDesc.Add(objNewMember, Descricao)
        CollIndex.Add(objNewMember, "k=" & Trim(objNewMember.Index))

        '    End If

        'return the object created
        Add = objNewMember
        objNewMember = Nothing
        'fimAdd:
        '    Exit Function
        '    Resume
        'errAdd:
        '    If Err.Number = 457 Then
        '      Resume fimAdd
        '    End If
    End Function

    Public ReadOnly Property Count() As Long
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Return CollDesc.Count
        End Get
    End Property

    Public ReadOnly Property ItemDescricao(ByVal vntIndexKey As Object) As Tarefa

        Get
            Return CollDesc(vntIndexKey)
            'On Error GoTo errItemDescricao
            'ItemDescricao = CollDesc(vntIndexKey)
            'fimItemDescricao:
            '      Exit Property
            'errItemDescricao:
            '      If err.Number = 5 Then
            '        Dim Vazio As New Tarefa
            '        ItemDescricao = Vazio
            '      End If
        End Get
    End Property

    Public ReadOnly Property ItemIndex(ByVal vntIndexKey As Object) As Tarefa
        'used when referencing an element in the collection
        'vntIndexKey contains either the Index or Key to the collection,
        'this is why it is declared as a Variant
        'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
        Get
            'ItemIndex = CollIndex("K=" & Trim(vntIndexKey))
            Return CollIndex("K=" & Trim(vntIndexKey))
        End Get
    End Property

    Private Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return CollIndex.GetEnumerator
    End Function

    Public Sub Remove(ByVal vntIndexKey As Object)
        'On Error GoTo fimRemove
        Dim vTarefa As Tarefa
        Dim tmp As Object

        If CollDesc.Count > 0 Then

            If IsNumeric(vntIndexKey) = True Then
                vTarefa = CollIndex(vntIndexKey)
                tmp = vTarefa.Descricao
                vTarefa = Nothing
                Call RemoveIndex(vntIndexKey)
                Call RemoveDesc(tmp)
            Else
                vTarefa = CollDesc(vntIndexKey)
                tmp = vTarefa.Index
                vTarefa = Nothing
                Call RemoveIndex(tmp)
                Call RemoveDesc(vntIndexKey)
            End If

        End If
        'fimRemove:
    End Sub

    Private Sub RemoveDesc(ByVal vntIndexKey As Object)
        CollDesc.Remove(vntIndexKey)
    End Sub

    Private Sub RemoveIndex(ByVal vntIndexKey As Object)
        CollIndex.Remove("K=" & vntIndexKey)
    End Sub

End Class

Public Class TarefaExemplo

    Public Sub exemplo()

        Dim Ts As New Tarefas
        Dim ob1 As New ob
        Dim ob2 As New ob

        ob1.text = "11"
        ob2.text = "12"
        Ts.Add("Item1", 1, "Processo1", ob1, ob2)
        ob1.text = "21"
        ob2.text = "22"
        Ts.Add("Item2", 2, "Processo2", ob1, ob2)
        ob1.text = "31"
        ob2.text = "32"
        Ts.Add("Item3", 3, "Processo3", ob1, ob2)
        ob1.text = "41"
        ob2.text = "42"
        Ts.Add("Item4", 4, "Processo4", ob1, ob2)
        ob1.text = "51"
        ob2.text = "52"
        Ts.Add("Item5", 5, "Processo5", ob1, ob2)
        ob1.text = "61"
        ob2.text = "62"
        Ts.Add("Item6", 6, "Processo6", ob1, ob2)

        'Vamos testar for next
        Dim t As Tarefa
        Dim txt As String
        For Each t In Ts

            txt = t.Status
            txt = t.progress.ToString
            txt = t.Descricao
            txt = t.Index.ToString
            txt = t.objectProgress.text
            txt = t.objectStatus.text

        Next


    End Sub

    Private Class ob

        Public text As String

        Public Sub Refresh()
            Beep()
        End Sub


    End Class

End Class