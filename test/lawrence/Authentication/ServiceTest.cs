﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.18020.
// 

namespace Authentication
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi")]
    [System.Xml.Serialization.XmlRootAttribute("RecMainService", Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi", IsNullable = false)]
    public partial class RecMainServiceType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private requestType requestField;

        private errorsType errorsField;

        private responseType responseField;

        /// <remarks/>
        public requestType request
        {
            get
            {
                return this.requestField;
            }
            set
            {
                this.requestField = value;
                this.RaisePropertyChanged("request");
            }
        }

        /// <remarks/>
        public errorsType errors
        {
            get
            {
                return this.errorsField;
            }
            set
            {
                this.errorsField = value;
                this.RaisePropertyChanged("errors");
            }
        }

        /// <remarks/>
        public responseType response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
                this.RaisePropertyChanged("response");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi")]
    public partial class requestType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string methodField;

        private string resourceField;

        private string attributesField;

        private string statusField;

        private string statusMessageField;

        /// <remarks/>
        public string method
        {
            get
            {
                return this.methodField;
            }
            set
            {
                this.methodField = value;
                this.RaisePropertyChanged("method");
            }
        }

        /// <remarks/>
        public string resource
        {
            get
            {
                return this.resourceField;
            }
            set
            {
                this.resourceField = value;
                this.RaisePropertyChanged("resource");
            }
        }

        /// <remarks/>
        public string attributes
        {
            get
            {
                return this.attributesField;
            }
            set
            {
                this.attributesField = value;
                this.RaisePropertyChanged("attributes");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }

        /// <remarks/>
        public string statusMessage
        {
            get
            {
                return this.statusMessageField;
            }
            set
            {
                this.statusMessageField = value;
                this.RaisePropertyChanged("statusMessage");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi")]
    public partial class InfoType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string departmentField;

        private string typeField;

        /// <remarks/>
        public string department
        {
            get
            {
                return this.departmentField;
            }
            set
            {
                this.departmentField = value;
                this.RaisePropertyChanged("department");
            }
        }

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("type");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi")]
    public partial class responseType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string disYearTermField;

        private string addressTypeField;

        private string unlistedField;

        private string address1Field;

        private string address2Field;

        private string phone_unlistedField;

        private string phoneField;

        private string regStatusField;

        private string regStatusDescField;

        private string regEligibilityField;

        private string regEligDescField;

        private string withdrawField;

        private string classStandingField;

        private string firstYrtField;

        private string lastYrtField;

        private string acadStandingField;

        private string termStandingField;

        private string numClassesField;

        private string numHoursField;

        private string semesterGpaField;

        private InfoType[] majorField;

        private InfoType[] creditListField;

        private InfoType[] classListField;

        private string modeField;

        private string editFlagField;

        private string buildModeField;

        private System.DateTime startField;

        private System.DateTime endField;

        private string yearTermField;

        private string currWithdrawField;

        private string currentYearTermField;

        private string studentLevelField;

        private string gradFullTimeField;

        private string gradStatusEffField;

        private string gradStatusExpField;

        private string termStandingEffField;

        private string termStandingExpField;

        private string deStatusField;

        private string tranBreakdownField;

        private string calcByuEarnedField;

        private string calcByuGradedField;

        private string calcByuPointsField;

        private string calcByuGpaField;

        private string calcTotalEarnedField;

        private string calcTotalGradedField;

        private string calcTotalPointsField;

        private string calcTotalGpaField;

        private string calcAddHrsField;

        private string calcAddPtsField;

        private string gpaClassesField;

        private InfoType[] yeartermListField;

        private InfoType[] displayListField;

        /// <remarks/>
        public string disYearTerm
        {
            get
            {
                return this.disYearTermField;
            }
            set
            {
                this.disYearTermField = value;
                this.RaisePropertyChanged("disYearTerm");
            }
        }

        /// <remarks/>
        public string addressType
        {
            get
            {
                return this.addressTypeField;
            }
            set
            {
                this.addressTypeField = value;
                this.RaisePropertyChanged("addressType");
            }
        }

        /// <remarks/>
        public string unlisted
        {
            get
            {
                return this.unlistedField;
            }
            set
            {
                this.unlistedField = value;
                this.RaisePropertyChanged("unlisted");
            }
        }

        /// <remarks/>
        public string address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
                this.RaisePropertyChanged("address1");
            }
        }

        /// <remarks/>
        public string address2
        {
            get
            {
                return this.address2Field;
            }
            set
            {
                this.address2Field = value;
                this.RaisePropertyChanged("address2");
            }
        }

        /// <remarks/>
        public string phone_unlisted
        {
            get
            {
                return this.phone_unlistedField;
            }
            set
            {
                this.phone_unlistedField = value;
                this.RaisePropertyChanged("phone_unlisted");
            }
        }

        /// <remarks/>
        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
                this.RaisePropertyChanged("phone");
            }
        }

        /// <remarks/>
        public string regStatus
        {
            get
            {
                return this.regStatusField;
            }
            set
            {
                this.regStatusField = value;
                this.RaisePropertyChanged("regStatus");
            }
        }

        /// <remarks/>
        public string regStatusDesc
        {
            get
            {
                return this.regStatusDescField;
            }
            set
            {
                this.regStatusDescField = value;
                this.RaisePropertyChanged("regStatusDesc");
            }
        }

        /// <remarks/>
        public string regEligibility
        {
            get
            {
                return this.regEligibilityField;
            }
            set
            {
                this.regEligibilityField = value;
                this.RaisePropertyChanged("regEligibility");
            }
        }

        /// <remarks/>
        public string regEligDesc
        {
            get
            {
                return this.regEligDescField;
            }
            set
            {
                this.regEligDescField = value;
                this.RaisePropertyChanged("regEligDesc");
            }
        }

        /// <remarks/>
        public string withdraw
        {
            get
            {
                return this.withdrawField;
            }
            set
            {
                this.withdrawField = value;
                this.RaisePropertyChanged("withdraw");
            }
        }

        /// <remarks/>
        public string classStanding
        {
            get
            {
                return this.classStandingField;
            }
            set
            {
                this.classStandingField = value;
                this.RaisePropertyChanged("classStanding");
            }
        }

        /// <remarks/>
        public string firstYrt
        {
            get
            {
                return this.firstYrtField;
            }
            set
            {
                this.firstYrtField = value;
                this.RaisePropertyChanged("firstYrt");
            }
        }

        /// <remarks/>
        public string lastYrt
        {
            get
            {
                return this.lastYrtField;
            }
            set
            {
                this.lastYrtField = value;
                this.RaisePropertyChanged("lastYrt");
            }
        }

        /// <remarks/>
        public string acadStanding
        {
            get
            {
                return this.acadStandingField;
            }
            set
            {
                this.acadStandingField = value;
                this.RaisePropertyChanged("acadStanding");
            }
        }

        /// <remarks/>
        public string termStanding
        {
            get
            {
                return this.termStandingField;
            }
            set
            {
                this.termStandingField = value;
                this.RaisePropertyChanged("termStanding");
            }
        }

        /// <remarks/>
        public string numClasses
        {
            get
            {
                return this.numClassesField;
            }
            set
            {
                this.numClassesField = value;
                this.RaisePropertyChanged("numClasses");
            }
        }

        /// <remarks/>
        public string numHours
        {
            get
            {
                return this.numHoursField;
            }
            set
            {
                this.numHoursField = value;
                this.RaisePropertyChanged("numHours");
            }
        }

        /// <remarks/>
        public string semesterGpa
        {
            get
            {
                return this.semesterGpaField;
            }
            set
            {
                this.semesterGpaField = value;
                this.RaisePropertyChanged("semesterGpa");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Info", IsNullable = false)]
        public InfoType[] Major
        {
            get
            {
                return this.majorField;
            }
            set
            {
                this.majorField = value;
                this.RaisePropertyChanged("Major");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Info", IsNullable = false)]
        public InfoType[] CreditList
        {
            get
            {
                return this.creditListField;
            }
            set
            {
                this.creditListField = value;
                this.RaisePropertyChanged("CreditList");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Info", IsNullable = false)]
        public InfoType[] ClassList
        {
            get
            {
                return this.classListField;
            }
            set
            {
                this.classListField = value;
                this.RaisePropertyChanged("ClassList");
            }
        }

        /// <remarks/>
        public string mode
        {
            get
            {
                return this.modeField;
            }
            set
            {
                this.modeField = value;
                this.RaisePropertyChanged("mode");
            }
        }

        /// <remarks/>
        public string editFlag
        {
            get
            {
                return this.editFlagField;
            }
            set
            {
                this.editFlagField = value;
                this.RaisePropertyChanged("editFlag");
            }
        }

        /// <remarks/>
        public string buildMode
        {
            get
            {
                return this.buildModeField;
            }
            set
            {
                this.buildModeField = value;
                this.RaisePropertyChanged("buildMode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
                this.RaisePropertyChanged("start");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime end
        {
            get
            {
                return this.endField;
            }
            set
            {
                this.endField = value;
                this.RaisePropertyChanged("end");
            }
        }

        /// <remarks/>
        public string yearTerm
        {
            get
            {
                return this.yearTermField;
            }
            set
            {
                this.yearTermField = value;
                this.RaisePropertyChanged("yearTerm");
            }
        }

        /// <remarks/>
        public string currWithdraw
        {
            get
            {
                return this.currWithdrawField;
            }
            set
            {
                this.currWithdrawField = value;
                this.RaisePropertyChanged("currWithdraw");
            }
        }

        /// <remarks/>
        public string currentYearTerm
        {
            get
            {
                return this.currentYearTermField;
            }
            set
            {
                this.currentYearTermField = value;
                this.RaisePropertyChanged("currentYearTerm");
            }
        }

        /// <remarks/>
        public string studentLevel
        {
            get
            {
                return this.studentLevelField;
            }
            set
            {
                this.studentLevelField = value;
                this.RaisePropertyChanged("studentLevel");
            }
        }

        /// <remarks/>
        public string gradFullTime
        {
            get
            {
                return this.gradFullTimeField;
            }
            set
            {
                this.gradFullTimeField = value;
                this.RaisePropertyChanged("gradFullTime");
            }
        }

        /// <remarks/>
        public string gradStatusEff
        {
            get
            {
                return this.gradStatusEffField;
            }
            set
            {
                this.gradStatusEffField = value;
                this.RaisePropertyChanged("gradStatusEff");
            }
        }

        /// <remarks/>
        public string gradStatusExp
        {
            get
            {
                return this.gradStatusExpField;
            }
            set
            {
                this.gradStatusExpField = value;
                this.RaisePropertyChanged("gradStatusExp");
            }
        }

        /// <remarks/>
        public string termStandingEff
        {
            get
            {
                return this.termStandingEffField;
            }
            set
            {
                this.termStandingEffField = value;
                this.RaisePropertyChanged("termStandingEff");
            }
        }

        /// <remarks/>
        public string termStandingExp
        {
            get
            {
                return this.termStandingExpField;
            }
            set
            {
                this.termStandingExpField = value;
                this.RaisePropertyChanged("termStandingExp");
            }
        }

        /// <remarks/>
        public string deStatus
        {
            get
            {
                return this.deStatusField;
            }
            set
            {
                this.deStatusField = value;
                this.RaisePropertyChanged("deStatus");
            }
        }

        /// <remarks/>
        public string tranBreakdown
        {
            get
            {
                return this.tranBreakdownField;
            }
            set
            {
                this.tranBreakdownField = value;
                this.RaisePropertyChanged("tranBreakdown");
            }
        }

        /// <remarks/>
        public string calcByuEarned
        {
            get
            {
                return this.calcByuEarnedField;
            }
            set
            {
                this.calcByuEarnedField = value;
                this.RaisePropertyChanged("calcByuEarned");
            }
        }

        /// <remarks/>
        public string calcByuGraded
        {
            get
            {
                return this.calcByuGradedField;
            }
            set
            {
                this.calcByuGradedField = value;
                this.RaisePropertyChanged("calcByuGraded");
            }
        }

        /// <remarks/>
        public string calcByuPoints
        {
            get
            {
                return this.calcByuPointsField;
            }
            set
            {
                this.calcByuPointsField = value;
                this.RaisePropertyChanged("calcByuPoints");
            }
        }

        /// <remarks/>
        public string calcByuGpa
        {
            get
            {
                return this.calcByuGpaField;
            }
            set
            {
                this.calcByuGpaField = value;
                this.RaisePropertyChanged("calcByuGpa");
            }
        }

        /// <remarks/>
        public string calcTotalEarned
        {
            get
            {
                return this.calcTotalEarnedField;
            }
            set
            {
                this.calcTotalEarnedField = value;
                this.RaisePropertyChanged("calcTotalEarned");
            }
        }

        /// <remarks/>
        public string calcTotalGraded
        {
            get
            {
                return this.calcTotalGradedField;
            }
            set
            {
                this.calcTotalGradedField = value;
                this.RaisePropertyChanged("calcTotalGraded");
            }
        }

        /// <remarks/>
        public string calcTotalPoints
        {
            get
            {
                return this.calcTotalPointsField;
            }
            set
            {
                this.calcTotalPointsField = value;
                this.RaisePropertyChanged("calcTotalPoints");
            }
        }

        /// <remarks/>
        public string calcTotalGpa
        {
            get
            {
                return this.calcTotalGpaField;
            }
            set
            {
                this.calcTotalGpaField = value;
                this.RaisePropertyChanged("calcTotalGpa");
            }
        }

        /// <remarks/>
        public string calcAddHrs
        {
            get
            {
                return this.calcAddHrsField;
            }
            set
            {
                this.calcAddHrsField = value;
                this.RaisePropertyChanged("calcAddHrs");
            }
        }

        /// <remarks/>
        public string calcAddPts
        {
            get
            {
                return this.calcAddPtsField;
            }
            set
            {
                this.calcAddPtsField = value;
                this.RaisePropertyChanged("calcAddPts");
            }
        }

        /// <remarks/>
        public string gpaClasses
        {
            get
            {
                return this.gpaClassesField;
            }
            set
            {
                this.gpaClassesField = value;
                this.RaisePropertyChanged("gpaClasses");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Info", IsNullable = false)]
        public InfoType[] YeartermList
        {
            get
            {
                return this.yeartermListField;
            }
            set
            {
                this.yeartermListField = value;
                this.RaisePropertyChanged("YeartermList");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Info", IsNullable = false)]
        public InfoType[] DisplayList
        {
            get
            {
                return this.displayListField;
            }
            set
            {
                this.displayListField = value;
                this.RaisePropertyChanged("DisplayList");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi")]
    public partial class errorType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string codeField;

        private string nameField;

        private string messageField;

        private string httpStatusCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
                this.RaisePropertyChanged("code");
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <remarks/>
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
                this.RaisePropertyChanged("message");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string httpStatusCode
        {
            get
            {
                return this.httpStatusCodeField;
            }
            set
            {
                this.httpStatusCodeField = value;
                this.RaisePropertyChanged("httpStatusCode");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "uri://byu/c/ry/ae/prod/records/cgi/recMain.cgi")]
    public partial class errorsType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private errorType errorField;

        /// <remarks/>
        public errorType error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
                this.RaisePropertyChanged("error");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}