function getcustomerContactsFilterData(SelectedCustomer) {
    var data = { Options: { Filters: [{ Property: 'CustomerId', Operation: 'Equal', Value: SelectedCustomer }] } };
    return data;
}
