Imports System
Imports System.Data

Namespace DotNetDataProviderTemplate
    Public Class TemplateParameter
        Implements IDataParameter

        Dim m_dbType As DbType = DbType.Object
        Dim m_direction As ParameterDirection = ParameterDirection.Input
        Dim m_fNullable As Boolean = False
        Dim m_sParamName As String
        Dim m_sSourceColumn As String
        Dim m_sourceVersion As DataRowVersion = DataRowVersion.Current
        Dim m_value As Object

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal parameterName As String, ByVal type As DbType)
            MyBase.New()

            m_sParamName = parameterName
            m_dbType = type
        End Sub

        Public Sub New(ByVal parameterName As String, ByVal value As Object)
            MyBase.New()

            m_sParamName = parameterName
            Me.Value = value
            ' Setting the value also infers the type.
        End Sub

        Public Sub New(ByVal parameterName As String, ByVal type As DbType, ByVal sourceColumn As String)
            MyBase.New()

            m_sParamName = parameterName
            m_dbType = dbType
            m_sSourceColumn = sourceColumn
        End Sub

        Public Property DbType() As DbType Implements IDataParameter.DbType
            Get
                Return m_dbType
            End Get
            Set(ByVal Value As DbType)
                m_dbType = value
            End Set
        End Property

        Public Property Direction() As ParameterDirection Implements IDataParameter.Direction
            Get
                Return m_direction
            End Get
            Set(ByVal Value As ParameterDirection)
                m_direction = value
            End Set
        End Property

        Public ReadOnly Property IsNullable() As Boolean Implements IDataParameter.IsNullable
            Get
                Return m_fNullable
            End Get
        End Property

        Public Property ParameterName() As String Implements IDataParameter.ParameterName
            Get
                Return m_sParamName
            End Get
            Set(ByVal Value As String)
                m_sParamName = value
            End Set
        End Property

        Public Property SourceColumn() As String Implements IDataParameter.SourceColumn
            Get
                Return m_sSourceColumn
            End Get
            Set(ByVal Value As String)
                m_sSourceColumn = value
            End Set
        End Property

        Public Property SourceVersion() As DataRowVersion Implements IDataParameter.SourceVersion
            Get
                Return m_sourceVersion
            End Get
            Set(ByVal Value As DataRowVersion)
                m_sourceVersion = value
            End Set
        End Property

        Public Property Value() As Object Implements IDataParameter.Value
            Get
                Return m_value
            End Get
            Set(ByVal Value As Object)
                m_value = value
                m_dbType = _inferType(value)
            End Set
        End Property

        Private Function _inferType(ByVal value As Object) As DbType
            Select Case (Type.GetTypeCode(value.GetType()))
                Case TypeCode.Empty
                    Throw New SystemException("Invalid data type")

                Case TypeCode.Object
                    Return DbType.Object

                Case TypeCode.DBNull
                Case TypeCode.Char
                Case TypeCode.[SByte]
                Case TypeCode.UInt16
                Case TypeCode.UInt32
                Case TypeCode.UInt64
                    ' Throw a SystemException for unsupported data types.
                    Throw New SystemException("Invalid data type")

                Case TypeCode.Boolean
                    Return DbType.Boolean

                Case TypeCode.Byte
                    Return DbType.Byte

                Case TypeCode.Int16
                    Return DbType.Int16

                Case TypeCode.Int32
                    Return DbType.Int32

                Case TypeCode.Int64
                    Return DbType.Int64

                Case TypeCode.Single
                    Return DbType.Single

                Case TypeCode.Double
                    Return DbType.Double

                Case TypeCode.Decimal
                    Return DbType.Decimal

                Case TypeCode.DateTime
                    Return DbType.DateTime

                Case TypeCode.String
                    Return DbType.String

                Case Else
                    Throw New SystemException("Value is of unknown data type")
            End Select
        End Function
    End Class
End Namespace
