Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Serialization
Imports DevExpress.Xpf.Docking

Namespace DXWpfApplication
	Partial Public Class MainWindow
		Inherits DXWindow
		Private Const LayoutPath As String = "layout.xml"
		Private Const AppName As String = "DXWpfApplication"
		'
		Public Sub New()
			InitializeComponent()
			targetPanel.SetValue(DXSerializer.SerializationProviderProperty, New PanelSerializationProvider())
		End Sub
		Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
			DXSerializer.SerializeSingleObject(targetPanel, LayoutPath, AppName)
		End Sub
		Private Sub ButtonRestore_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
			DXSerializer.DeserializeSingleObject(targetPanel, LayoutPath, AppName)
		End Sub
	End Class
	Friend Class PanelSerializationProvider
		Inherits SerializationProvider
		Protected Overrides Function GetSerializationID(ByVal dObj As DependencyObject) As String
			Return "customPanelProperties"
		End Function
		Protected Overrides Sub OnStartSerializing(ByVal dObj As DependencyObject)
			Dim panel As LayoutPanel = CType(dObj, LayoutPanel)
			Using ms As New MemoryStream()
				DXSerializer.Serialize(panel.Control, ms, "customLayout", New DXOptionsLayout())
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
			If (Not String.IsNullOrEmpty(layout)) Then
				Using ms As New MemoryStream(Convert.FromBase64String(layout))
					DXSerializer.Deserialize(panel.Control, ms, "customLayout", New DXOptionsLayout())
				End Using
			End If
			panel.Tag = Nothing
		End Sub
		Protected Overrides Sub OnCustomGetSerializableProperties(ByVal dObj As DependencyObject, ByVal e As CustomGetSerializablePropertiesEventArgs)
			e.SetPropertySerializable("Tag", New DXSerializable())
		End Sub
	End Class
	Friend Class TestData
		Private privateText As String
		Public Property Text() As String
			Get
				Return privateText
			End Get
			Set(ByVal value As String)
				privateText = value
			End Set
		End Property
		Private privateNumber As Integer
		Public Property Number() As Integer
			Get
				Return privateNumber
			End Get
			Set(ByVal value As Integer)
				privateNumber = value
			End Set
		End Property
	End Class
End Namespace