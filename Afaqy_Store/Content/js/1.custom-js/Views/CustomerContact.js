function getcustomerContactsFilterData(SelectedCustomer) {
    var data = { Options: { Filters: [{ Property: 'aux_id', Operation: 'Equal', Value: SelectedCustomer }] } };
    return data;
}
