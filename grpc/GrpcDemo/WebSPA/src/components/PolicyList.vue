<template>
    <div>
        <div class="filter-container">
            <h4>Search policies</h4>
            <b-form inline>
                <b-input v-model="filterFields.number" placeholder="Policy number" class="mr-sm-2 mb-sm-0 search-input"></b-input>
                <b-input v-model="filterFields.policyHolder" placeholder="Policy Holder" class="mr-sm-2 mb-sm-0 search-input"></b-input>
                <b-button v-on:click="search()" variant="primary" class="search-button">Search</b-button>
            </b-form>
        </div>
    </div>
</template>

<script>
    import {HTTP} from './http/ApiClient';
    const AND = "%20AND%20";
    export default {
        name: "PolicyList",
        data() {
            return {
                fields: [
                    {key: 'policyNumber'},
                    {key: 'policyStartDate'},
                    {key: 'policyEndDate'},
                    {key: 'policyHolder'}
                ],
                policies: [],
                filterFields: {
                    policyHolder: '',
                    number: ''
                }
            }
        },
        created() {

        },
        methods: {
            showDetails(record) {
                this.$router.push({name: 'policyDetails', params: { policyNumber: record.policyNumber }});
            },
            search() {
                let queryString = '';
                queryString = this.addCriteria(queryString, this.filterFields.policyHolder);
                queryString = this.addCriteria(queryString, this.filterFields.number);

                this.runSearch(this.formatQueryString(queryString));
            },
            runSearch(queryString = '') {
                HTTP.get('PolicySearch' + queryString).then(response => {
                    this.policies = response.data.policies;
                })
            },
            addCriteria(queryString, criteria) {
                if (criteria !== '') {
                    queryString += criteria + AND;
                }
                return queryString;
            },
            formatQueryString(queryString) {
                if (queryString.endsWith(AND)) {
                    queryString = queryString.substring(0, queryString.length - AND.length);
                }
                console.log(queryString);
                return queryString !== '' ? '?q=' + queryString : '';
            }
        }
    }
</script>

<style scoped lang="scss">
    .filter-container {
        margin-top: 20px;
        margin-bottom: 20px;
    }
    .search-input {
        width: 45% !important;
    }
    .search-button {
        width: 8% !important;
    }
</style>
