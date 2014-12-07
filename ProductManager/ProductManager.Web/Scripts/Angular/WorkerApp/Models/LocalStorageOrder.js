function LocalStorageOrder() {
    this.productTypeGroups = [];
    this.discount = 0;
    this.totalPrice = 0;
}

function ProductTypeGroup(indexId,productTypeId, typeName,calculationType) {
    this.indexId = indexId;
    this.productTypeId = productTypeId;
    this.typeName = typeName;
    this.calculationType = calculationType;
    this.orderlines = [];
}

function AddOn(id, name, price) {
    this.id = id;
    this.name = name;
    this.price = price;
}


function OrderLine(indexId, name, width, height,amount,size,price, addons, additionalInfo ) {
    this.id = indexId;
    this.name = name;
    this.width = width;
    this.height = height;
    this.amount = amount;
    this.size = size;
    this.price = price;
    this.addons = addons;
    this.additionalInfo = additionalInfo;
}