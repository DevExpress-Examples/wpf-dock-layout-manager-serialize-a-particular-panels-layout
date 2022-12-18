<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128643832/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2320)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WPF Dock Layout Manager - Serialize a Particular Panel's Layout

This example serializes a particular panel layout.

In this example, a [GridControl](https://docs.devexpress.com/WPF/6084/controls-and-libraries/data-grid?p=netframework) is the LayoutPanel's content, and a [DockLayoutManager](https://docs.devexpress.com/WPF/DevExpress.Xpf.Docking.DockLayoutManager) saves/restores only this panel:

```C#
void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e) {
    DXSerializer.SerializeSingleObject(targetPanel, LayoutPath, AppName);
}
void ButtonRestore_Click(object sender, System.Windows.RoutedEventArgs e) {
    DXSerializer.DeserializeSingleObject(targetPanel, LayoutPath, AppName);
}

```

In this example, a custom serialization provider implements extended capabilities to control the save/restore layout process:


```C#
targetPanel.SetValue(DXSerializer.SerializationProviderProperty, new PanelSerializationProvider());

```

The **PanelSerializationProvider** class implements serialization logic that stores the panel's content layout to the `Tag` property and restores it back.

<!-- default file list -->
## Files to Look At

* [MainWindow.xaml](./CS/DX%20WPF%20Application10/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/DX%20WPF%20Application10/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/DX%20WPF%20Application10/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/DX%20WPF%20Application10/MainWindow.xaml.vb))
<!-- default file list end -->

## Documentation
- [Save and Restore the Layout of Dock Panels and Controls](https://docs.devexpress.com/WPF/7059/controls-and-libraries/layout-management/dock-windows/miscellaneous/saving-and-restoring-the-layout-of-dock-panels-and-controls)
- [Save/Restore Control Layout Overview](https://docs.devexpress.com/WPF/7391/common-concepts/save-and-restore-layouts)

## More Examples

- [WPF Dock Layout Manager - Save and Restore the DockLayoutManager's Layout](https://github.com/DevExpress-Examples/wpf-dock-layout-manager-save-and-restore-the-dock-layout-managers-layout)
