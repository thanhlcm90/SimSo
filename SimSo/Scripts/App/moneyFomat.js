String.prototype.splice = function (idx, rem, s) {
    return (this.slice(0, idx) + s + this.slice(idx + Math.abs(rem)));
};
String.prototype.moneyFormat = function (c) {
    var len = this.length;
    var temp = this;
    for (i = len - 3; i > 0; i -= 3) {
        temp = temp.splice(i, 0, c);
    }
    return temp;
};