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


