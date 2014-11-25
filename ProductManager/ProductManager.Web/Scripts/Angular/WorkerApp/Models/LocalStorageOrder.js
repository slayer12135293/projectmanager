function LocalStorageOrder() {
    this.productTypeGroups = [];
    this.totalPrice = 0;
}

function ProductTypeGroup(indexId,productTypeId, typeName,calculationType) {
    this.indexId = indexId;
    this.productTypeId = productTypeId;
    this.typeName = typeName;
    this.calculationType = calculationType;
    this.orderlines = [];
    this.addOns = [];
}

function AddOn(id, name, price) {
    this.id = id;
    this.name = name;
    this.price = price;
}
