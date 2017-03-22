class CustomerContact {
    constructor(CustomerContactId,DolphinId,CustomerId,ContactName_en, ContactName_ar, Position_en, Position_ar, IsDefault, IsBlock, ContactDetails) {
        this.CustomerContactId = CustomerContactId;
        this.DolphinId = DolphinId;
        this.CustomerId = CustomerId;
        this.ContactName_en = ContactName_en;
        this.ContactName_ar = ContactName_ar;
        this.Position_en = Position_en;
        this.Position_ar = Position_ar;
        this.IsDefault = IsDefault;
        this.IsBlock = IsBlock;
        this.ContactDetails = ContactDetails;
    }
}

class CustomerContactDetails {
    constructor(CustomerContactDetailsId, DolphinId, CustomerContactId, ContactMethodId, ContactContent, IsDefault, IsBlock, ContactMethods) {
        this.CustomerContactDetailsId = CustomerContactDetailsId;
        this.DolphinId = DolphinId;
        this.CustomerContactId = CustomerContactId;
        this.ContactMethodId = ContactMethodId;
        this.ContactContent = ContactContent;
        this.IsDefault = IsDefault;
        this.IsBlock = IsBlock;
        this.ContactMethods = ContactMethods;
    }
}