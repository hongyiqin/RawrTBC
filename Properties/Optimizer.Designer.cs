﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rawr.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Optimizer : global::System.Configuration.ApplicationSettingsBase {
        
        private static Optimizer defaultInstance = ((Optimizer)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Optimizer())));
        
        public static Optimizer Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool WarningsEnabled {
            get {
                return ((bool)(this["WarningsEnabled"]));
            }
            set {
                this["WarningsEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("150")]
        public int Thoroughness {
            get {
                return ((int)(this["Thoroughness"]));
            }
            set {
                this["Thoroughness"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool OverrideRegem {
            get {
                return ((bool)(this["OverrideRegem"]));
            }
            set {
                this["OverrideRegem"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string CalculationToOptimize {
            get {
                return ((string)(this["CalculationToOptimize"]));
            }
            set {
                this["CalculationToOptimize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool OverrideReenchant {
            get {
                return ((bool)(this["OverrideReenchant"]));
            }
            set {
                this["OverrideReenchant"] = value;
            }
        }
    }
}
