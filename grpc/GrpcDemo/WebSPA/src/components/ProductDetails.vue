<template>

</template>

<script>
    import {HTTP} from './http/ApiClient'
    export default {
        name: "ProductDetails",
        props: {
            productCode: String
        },
        data() {
            return {
                productDetails: {},
                answers: [],
                mode: 'EDIT',
                price: {
                    amountToPay: null
                },
                policyFrom: '',
                policyTo: '',
                offerNumber: ''
            }
        },
        created() {
            HTTP.get('products/' + this.productCode).then(response => {
                this.productDetails = response.data;
                if (!this.productDetails.questions)
                    return;

                for (let i = 0; i < this.productDetails.questions.length; i++) {
                    this.answers.push({
                        answer: null,
                        question: this.productDetails.questions[i]
                    })
                }
            });
        },
        methods: {
            backToEdit: function () {
                this.mode = 'EDIT';
            },
            createRequest: function () {
                const request = {
                    'productCode': this.productDetails.code,
                    'policyFrom': this.policyFrom,
                    'policyTo': this.policyTo,
                    'selectedCovers': [],
                    'answers': []
                }
                for (let i = 0; i < this.productDetails.covers.length; i++) {
                    request.selectedCovers.push(this.productDetails.covers[i].code)
                }

                for (let j = 0; j < this.answers.length; j++) {
                    request.answers.push({
                        'questionCode': this.answers[j].question.questionCode,
                        'questionType': this.answers[j].question.questionType,
                        'answer': this.answers[j].answer
                    })
                }

                return request;
            },
            calculatePrice: function () {
                HTTP.post('offers', this.createRequest()).then(response => {
                    this.mode = 'VIEW';
                    this.price.amountToPay = response.data.totalPrice;
                    this.offerNumber = response.data.offerNumber;
                }, () => {
                    alert('Bad Things Happened');
                });
            },
            createOffer: function () {
                this.$router.push({name: 'createPolicy', params: { offerNumber: this.offerNumber }});
            }
        }
    }
</script>

<style scoped lang="scss">
    .margin-top-10 {
        margin-top: 10px;
    }
</style>
