<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128643832/11.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2320)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[MainWindow.xaml](./CS/DX WPF Application10/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/DX WPF Application10/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/DX WPF Application10/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/DX WPF Application10/MainWindow.xaml.vb))
<!-- default file list end -->
# How to serialize a particular panel layout


<p>This example demonstrates how to serialize a particular panel layout.</p><p>In this example, a GridControl is used as the content for a particular LayoutPanel, <br />
and a save/restore action is invoked only for this panel:<br />


```C#
<newline/>
        void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e) {<newline/>
            DXSerializer.SerializeSingleObject(targetPanel, LayoutPath, AppName);<newline/>
        }<newline/>
        void ButtonRestore_Click(object sender, System.Windows.RoutedEventArgs e) {<newline/>
            DXSerializer.DeserializeSingleObject(targetPanel, LayoutPath, AppName);<newline/>
        }<newline/>

```

</p><p>By default, this action has no effect, but in this example, a special serialization provider is used to provide extended <br />
capabilities to control the saving/restoring process: <br />


```C#
<newline/>
      targetPanel.SetValue(DXSerializer.SerializationProviderProperty, new PanelSerializationProvider());<newline/>

```

</p><p>The PanelSerializationProvider class implements some serialization logic that stores the panel's content layout to the Tag property and restores it back.</p>

<br/>


