   M S B A M L   `   `   `  ����k  gAlternet.Scripter.Integration.Wpf.v9, Version=9.5.0.0, Culture=neutral, PublicKeyToken=8032721e70924a63�]clr-namespace:Alternet.Scripter.Integration.Wpf;assembly=Alternet.Scripter.Integration.Wpf.v9!Alternet.Scripter.Integration.Wpf  5   	   J FDebuggerExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DebuggerExampleDebuggerExample 5   	       DebuggerExample.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ] YAlternet.Editor.Wpf.v9, Version=9.5.0.0, Culture=neutral, PublicKeyToken=8032721e70924a639alternet*http://schemas.alternetsoft.com/editor/wpf  5   	   fai]clr-namespace:Alternet.Scripter.Integration.Wpf;assembly=Alternet.Scripter.Integration.Wpf.v9  5   	   )localclr-namespace:DebuggerExample  5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � 	Resources 5      ,  &DebuggerExample.VisiblefMatchConverter   VisiblefMatchConverter �� 
TargetType �� Property �� Value��%�  &	        '��      '�� �     '�� e      5   
   6'   �� 5   
   " ��6   .�5      ��  � ��$ 35��6'   �� 5       � ��$ 70��6&   �� 5       � ��$ 0,5,5,5q�6'   �� 5       � ��$ Center=�69   5      �� 5   
   " ��6   .�5      ��  � ��$ 35��6'   �� 5       � ��$ 70��6&   �� 5       � ��$ 0,5,5,5q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 22��6'   �� 5       � ��$ 5q�6'   5      5      .��5      � $��10q�6    � RowDefinitions 5    
   �� 5!      $
��Auto �6   �� 5"      .+�5&   
   ]� $��0��6   .+�5'      �� -   5        $��cbMode��5'      .@�5(      ��  �� 
IsSelected . .��61   Run66   �� 5)      .��6   Debug6&   5*      �� 5+      -   5        $��btnRun��5+   +   $
��Left=�6   $	��Run��69   $	��100��6\   y�5,      ��  �� ElementName$ cbMode�� �� Path$ SelectedIndex�	 �� 	Converter#	 [  
 �� ConverterParameter$
 0���� 5-      -   5        $��btnRunDebug��5-   +   $
��Left=�6   $��Run/Continue��6>   $	��100��6o   y�5.      �� $ cbMode��$ SelectedIndex�#	 [  $
 1���� 5/      -   5        $��btnStop��5/   +   $
��Left=�6   $
��Stop��6:   y�50      �� $ cbMode��$ SelectedIndex�#	 [  $
 1���� 51      -   5        $��btnStepInto��51   +   $
��Left=�6   $��StepInto��6>   y�52      �� $ cbMode��$ SelectedIndex�#	 [  $
 1���� 53      -   5        $��btnStepOver��53   +   $
��Left=�6   $��StepOver��6>   y�54      �� $ cbMode��$ SelectedIndex�#	 [  $
 1���� 55      -   5        $��
btnStepOut��55   +   $
��Left=�6   $��StepOut��6=   y�56      �� $ cbMode��$ SelectedIndex�#	 [  $
 1���� 57      58      $  Alternet.Editor.Wpf.TextEditor  5;   
   -   5        $��
textEditor��5;   +   $��1��6   $��Consolas)�6=   $
��10pt(�6S   $��0q�5<      
������    AllowOutlining .    LineNumbersVisible .    LineModificatorsVisible . !   AllowHorizontalEditorSplit .     AllowVerticalEditorSplit .  $��Stretch=�5?      $��Stretch=�6<   y�5@      �� $ cbMode��$ SelectedIndex�#	 [  $
 0��5   /Alternet.Scripter.Integration.Wpf.DebugCodeEdit  5D   
   -	   5        $��debugEditor��5D   (   $��1��6   $��Consolas)�6;   $
��10pt(�6Q   $��0q�5E      
������  .  .  .  .   .  $��Stretch=�5H      $��Stretch=�69   y�5I      �� $ cbMode��$ SelectedIndex�#	 [  $
 1��5J      5L      