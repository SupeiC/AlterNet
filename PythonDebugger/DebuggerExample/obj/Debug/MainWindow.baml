   M S B A M L   `   `   `  ����k  gAlternet.Scripter.Integration.Wpf.v9, Version=9.5.0.0, Culture=neutral, PublicKeyToken=8032721e70924a63�]clr-namespace:Alternet.Scripter.Integration.Wpf;assembly=Alternet.Scripter.Integration.Wpf.v9!Alternet.Scripter.Integration.Wpf  5   	   J FDebuggerExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DebuggerExampleDebuggerExample 5   	       DebuggerExample.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ] YAlternet.Editor.Wpf.v9, Version=9.5.0.0, Culture=neutral, PublicKeyToken=8032721e70924a639alternet*http://schemas.alternetsoft.com/editor/wpf  5   	   fai]clr-namespace:Alternet.Scripter.Integration.Wpf;assembly=Alternet.Scripter.Integration.Wpf.v9  5   	   )localclr-namespace:DebuggerExample  5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � 	Resources 5       �� 
TargetType �� Property �� Value��%  '��       '�� �     �� 5   
   " ��6   .�5      ��  � ��$ 35��6'   �� 5       � ��$ 70��6&   �� 5       � ��$ 0,5,5,5q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 22��6'   �� 5       � ��$ 5q�6'   5      5      .��5      ]� $��10q�6   .+�5   
   ]� $	��Top=�6   .+�5      �� -   5        $��btnRun��5   +   $
��Left=�6   $��Run/Continue��69   $	��100��6e   �� 5      -   5        $��btnStop��5   +   $
��Left=�6   $
��Stop��6:   6I   �� 5      -   5        $��btnStepInto��5   +   $
��Left=�6   $��StepInto��6>   6Q   �� 5      -   5        $��btnStepOver��5   +   $
��Left=�6   $��StepOver��6>   6Q   �� 5      -   5        $��
btnStepOut��5   +   $
��Left=�6   $��StepOut��6=   6O   �� 5      5       5   /Alternet.Scripter.Integration.Wpf.DebugCodeEdit  5#   
   -   5        $��debugEditor��5#   (   $��0��6   $��Consolas)�6;   $
��10pt(�6Q   $��0q�5$      
������ $  Alternet.Editor.Wpf.TextEditor   AllowOutlining .    LineNumbersVisible .    LineModificatorsVisible . !   AllowHorizontalEditorSplit .  	   AllowVerticalEditorSplit	 .  $��Stretch=�5'      $��Stretch=�69   5)      5+      