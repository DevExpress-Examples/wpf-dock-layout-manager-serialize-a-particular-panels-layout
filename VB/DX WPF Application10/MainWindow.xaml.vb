Imports System
Imports System.IO
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Serialization
Imports DevExpress.Xpf.Docking

Namespace DXWpfApplication

    Public Partial Class MainWindow
        Inherits DXWindow

        Const LayoutPath As String = "layout.xml"

        Const AppName As String = "DXWpfApplication"

        '
        Public Sub New()
            Me.InitializeComponent()
            Me.targetPanel.SetValue(DXSerializer.SerializationProviderProperty, New PanelSerializationProvider())
        End Sub

        Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call DXSerializer.SerializeSingleObject(Me.targetPanel, LayoutPath, AppName)
        End Sub

        Private Sub ButtonRestore_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call DXSerializer.DeserializeSingleObject(Me.targetPanel, LayoutPath, AppName)
        End Sub
    End Class

    Friend Class PanelSerializationProvider
        Inherits SerializationProvider

        Protected Overrides Function GetSerializationID(ByVal dObj As DependencyObject) As String
            Return "customPanelProperties"
        End Function

        Protected Overrides Sub OnStartSerializing(ByVal dObj As DependencyObject)
            Dim panel As LayoutPanel = CType(dObj, LayoutPanel)
            Using ms As MemoryStream = New MemoryStream()
                Call DXSerializer.Serialize(panel.Control, ms, "customLayout", New DXOptionsLayout())
                ms.Seek(0, SeekOrigin.Begin)
                panel.Tag = Convert.ToBase64String(ms.ToArray())
            End Using

            MyBase.OnStartSerializing(dObj)
        End Sub

        Protected Overrides Sub OnEndSerializing(ByVal dObj As DependencyObject)
            MyBase.OnEndSerializing(dObj)
            CType(dObj, LayoutPanel).Tag = Nothing
        End Sub

        Protected Overrides Sub OnEndDeserializing(ByVal dObj As DependencyObject, ByVal restoredVersion As String)
            MyBase.OnEndDeserializing(dObj, restoredVersion)
            Dim panel As LayoutPanel = CType(dObj, LayoutPanel)
            Dim layout As String = TryCast(panel.Tag, String)
            If Not String.IsNullOrEmpty(layout) Then
                Using ms As MemoryStream = New MemoryStream(Convert.FromBase64String(layout))
                    Call DXSerializer.Deserialize(panel.Control, ms, "customLayout", New DXOptionsLayout())
                End Using
            End If

            panel.Tag = Nothing
        End Sub

        Protected Overrides Sub OnCustomGetSerializableProperties(ByVal dObj As DependencyObject, ByVal e As CustomGetSerializablePropertiesEventArgs)
            e.SetPropertySerializable("Tag", New DXSerializable())
        End Sub
    End Class

    Friend Class TestData

        Public Property Text As String

        Public Property Number As Integer
    End Class
End Namespace
