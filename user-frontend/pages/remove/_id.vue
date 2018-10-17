<template>
  <v-layout
    column
    justify-center
    align-center>
    <v-btn @click="back" small>Back</v-btn>
         <v-data-table
    :headers="headers"
    :items="events"
    hide-actions
    class="elevation-1"
    disable-initial-sort

  >
    <template slot="items" slot-scope="props">
      <td>{{ props.item.memberInfo.firstName }}</td>
      <td class="text-xs-right">{{ props.item.memberInfo.lastName }}</td>
      <td class="text-xs-right">{{ props.item.status }}</td>
      <v-btn @click.native="remove(props.item._id)">Remove</v-btn>
    </template>
  </v-data-table>
          </v-layout>
  
</template>

<script>
import axios from 'axios'
import { mapMutations } from 'vuex'
import { mapFields } from 'vuex-map-fields'

export default {
  data() {
    return {
      headers: [
        { text: 'First Name', value: 'memberInfo.firstName' },
        { text: 'Last Name', value: 'memberInfo.lastName' },
        { text: 'Signup Status', value: 'status' },
        { text: 'Remove', value: 'remove' }
      ],
      events: []
    }
  },
  mounted() {
    this.getData()
  },
  computed: {
    // The `mapFields` function takes an array of
    // field names and generates corresponding
    // computed properties with getter and setter
    // functions for accessing the Vuex store.
  },
  methods: {
    getData: function() {
      $nuxt.$loading.start()
      axios
        .post(
          process.env.baseUrl + 'api/getPeopleList',
          { event_id: this.$route.params.id }
        )
        .then(response => {
          $nuxt.$loading.finish()
          this.events = response.data.res
        })
        .catch(function(error) {
          console.log(error)
        })
    },
    back: function() {
      this.$router.go(-1)
    },
    remove: function(_id) {
      $nuxt.$loading.start()
      axios
        .post(process.env.baseUrl + 'api/removeSignup', {
          _id: _id
        })
        .then(response => {
          $nuxt.$loading.finish()
          this.getData();
        })
        .catch(function(error) {
          console.log(error)
        })
    }
  }
}
</script>