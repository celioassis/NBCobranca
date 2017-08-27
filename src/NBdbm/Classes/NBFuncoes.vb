Option Explicit On

Friend Class NBFuncoes

    'Esta fun��o retorna somente os n�mero
    'de uma seq��ncia Alfanum�rica
    Friend Shared Function soNumero(ByVal string_ As String) As String

        Dim i As Double
        Dim str As New Text.StringBuilder
        Try
            For i = 1 To Len(string_)

                If IsNumeric(Mid(string_, i, 1)) = True Then
                    str.Append(Mid(string_, i, 1))
                End If
            Next
            Return str.ToString
        Catch ex As Exception
            Dim nbEx As New NBexception("N�o foi poss�vel retirar os N�meros da String", ex)
            nbEx.Source = "NBFuncoes.soNumero"
            Throw nbEx
        End Try
    End Function

    Friend Shared Function formataData(ByVal date_ As Date, ByVal formato As DateFormat) As String
        Return date_.ToString(formato)
    End Function

    Friend Shared Function validaCPFCNPJ(ByVal cpfcnpj As String, ByVal pessoaFisica As Boolean) As String

        Dim str As String

        str = soNumero(cpfcnpj).ToString
        If pessoaFisica And Len(str) < 12 Then
            str = Format(Val(str), "00000000000")
        ElseIf Len(str) <= 15 Then
            str = Format(Val(str), "000000000000000")
        Else
            Dim mNBEx As New NBexception("CNPJ_CPF inv�lido")
            mNBEx.Source = "NBFuncoes.validaCPFCNPJ"
            Throw mNBEx
        End If
        Return str
    End Function

    Friend Shared Sub SalvaArquivo(ByVal arqName As String, ByVal StrWrite As String)
        Dim mStream As System.IO.Stream

        mStream = System.IO.File.OpenWrite(arqName)

        Dim SrWriter As System.IO.StreamWriter = New System.IO.StreamWriter(mStream, System.Text.Encoding.Default)

        SrWriter.Write(StrWrite)

        SrWriter.Flush()
        SrWriter.Close()

    End Sub

    Private Shared Function f_CPFCGC(ByVal CPFCNPJ As String, ByVal NoMsg As Boolean) As Boolean

        Dim CPF1 As String = "", CPF2 As String = ""
        Dim Soma As Integer, Digito As Integer
        Dim I As Integer, J As Integer
        Dim ContIni As Integer, ContFim As Integer
        Dim Controle As String = ""
        Dim CGC1 As String = "", CGC2 As String = ""
        'Dim TELAS As Form
        Dim Add As Integer, VERIFICADOR As Integer
        Dim H As Integer, M As Integer
        Dim CO As String = "", mult As String = ""
        'TELAS = Screen.ActiveForm

        Dim Nl$

        If Len(CPFCNPJ) = 11 Or Len(CPFCNPJ) = 14 Then
            CPFCNPJ = Format(soNumero(CPFCNPJ).ToString, "00000000000")
            CPF1 = Left$(CPFCNPJ, 9)
            CPF2 = Right$(CPFCNPJ, 2)
            ContIni = 2
            ContFim = 10
            For J = 1 To 2
                Soma = 0
                For I = ContIni To ContFim
                    Soma = Soma + (Val(Mid$(CPF1, I - J, 1)) * (ContFim + 1 + J - I))
                Next I

                If J = 2 Then Soma = Soma + (2 * Digito)
                Digito = (Soma * 10) Mod 11
                If Digito = 10 Then Digito = 0
                Controle = Controle + Trim$(Str$(Digito))
                'Valores limite para I para o c�lculo do segundo d�gito
                ContIni = 3
                ContFim = 11
            Next J

            Nl$ = Chr(13) + Chr(10) 'Nova Linha

            If Controle <> CPF2 Then
                If NoMsg = False Then
                    Beep()
                    MsgBox("CPF inv�lido", 48, "             Mensagem do Sistema             ")
                End If
                f_CPFCGC = False
                Exit Function
            End If

        ElseIf 14 <= Len(CPFCNPJ) Or Len(CPFCNPJ) <= 19 Then
            CPFCNPJ = Format(soNumero(CPFCNPJ).ToString, "000000000000000")
            CGC1 = Left$(CPFCNPJ, 13)
            CGC2 = Right$(CPFCNPJ, 2)
            mult = "6543298765432"
            CO = ""
            For M = 1 To 2
                Add = 0
                For H = 1 To 13
                    Add = Add + (Val(Mid$(CGC1, H, 1)) * Val(Mid$(mult, H, 1)))
                Next H
                If M = 2 Then Add = Add + (2 * VERIFICADOR)
                VERIFICADOR = (Add * 10) Mod 11
                If VERIFICADOR = 10 Then VERIFICADOR = 0
                CO = CO + Trim$(Str$(VERIFICADOR))
                'Sequ�ncia de multplicadores para
                'o c�lculo do segundo d�gito
                mult = "76543298765432"
            Next M
            Nl$ = Chr(13) + Chr(10) 'Nova linha
            If CO <> CGC2 Then
                If NoMsg = False Then
                    Beep()
                    MsgBox("CNPJ inv�lido", 48, "             Mensagem do Sistema             ")
                End If
                f_CPFCGC = False
                Exit Function
            End If

            'txt1.Mask = "00\.000\.000\/0000\-00;;"
            'DoCmd CalcelEvent
            f_CPFCGC = True
            Exit Function
        Else

            If NoMsg = False Then
                Beep()
                MsgBox("N�mero de D�gitos inv�lido.", 48)
            End If

            f_CPFCGC = False
            Exit Function
        End If

        f_CPFCGC = True

    End Function

    Friend Shared Function decripto(ByVal string_ As String) As String
        'Descriptografa uma string contida na vari�vel "string_"

        Dim Key As String = ""
        Dim cont As Long
        Dim cont2 As Long
        Dim a As String = ""
        Dim b As String = ""
        Dim StrCript As String = ""

        Key = "d��fRDk��T� ��J���W��&z�k(N�Fa���|n4�O(=1���FWMH�#��b�66�+c|�/�[^��:���γ��J˥����x@-&��+�L���9P��f��\��jg l>4��~�h�n=;3�E��I��7�5��߿�?U�#{,U��w�:p���.K��apI<�SͺY.��i���r}�!�������$A`�X8���%5�2)uq�2��vC)S�_�}ح*~�h\9���ys��x����/G�v�<��_�T�Ee3�AKrZ�V��RtM����C��u��z��'��eݼ���Z���HX��l�i*묫o��8Q���]NO0`�^Yb�0]dq-%��;崡j��m>[�D,'L����o�ys�t@�G�{�QmPB��g�7$1�VwB��ĸc?!��"

        For cont = 1 To Len(string_)
            a = Mid(string_, cont, 1)

            For cont2 = 2 To Len(Key) Step 2
                If Mid(Key, cont2, 1) = a Then
                    b = Mid(Key, cont2 - 1, 1)
                    Exit For
                End If

            Next cont2

            StrCript = StrCript & b
        Next

        Return StrCript

    End Function

    Friend Shared Function cripto(ByVal string_ As String) As String
        'Criptografa uma string contida na vari�vel "string_"

        Dim Key As String = ""
        Dim cont As Long
        Dim cont2 As Long
        Dim a As String = ""
        Dim b As String = ""
        Dim StrCript As String = ""

        Key = "d��fRDk��T� ��J���W��&z�k(N�Fa���|n4�O(=1���FWMH�#��b�66�+c|�/�[^��:���γ��J˥����x@-&��+�L���9P��f��\��jg l>4��~�h�n=;3�E��I��7�5��߿�?U�#{,U��w�:p���.K��apI<�SͺY.��i���r}�!�������$A`�X8���%5�2)uq�2��vC)S�_�}ح*~�h\9���ys��x����/G�v�<��_�T�Ee3�AKrZ�V��RtM����C��u��z��'��eݼ���Z���HX��l�i*묫o��8Q���]NO0`�^Yb�0]dq-%��;崡j��m>[�D,'L����o�ys�t@�G�{�QmPB��g�7$1�VwB��ĸc?!��"

        For cont = 1 To Len(string_)
            a = Mid(string_, cont, 1)

            For cont2 = 1 To Len(Key) Step 2
                If Mid(Key, cont2, 1) = a Then
                    b = Mid(Key, cont2 + 1, 1)
                    Exit For
                End If

            Next cont2

            StrCript = StrCript & b
        Next

        Return StrCript

    End Function



End Class

