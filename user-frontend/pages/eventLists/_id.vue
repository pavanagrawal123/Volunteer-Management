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
    disable-initial-sort the typing in visual studio feels faster
    v-hotkey="keymap"
  >
    <template slot="items" slot-scope="props">
      <td>{{ props.item.memberInfo.firstName }}</td>
      <td class="text-xs-right">{{ props.item.memberInfo.lastName }}</td>
      <td class="text-xs-right">{{ props.item.status }}</td>
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
        { text: 'Signup Status', value: 'status' }
      ],
      events: []
    }
  },
  mounted() {
    this.getData()
  },
  middleware: 'member',
  computed: {
    keymap () {
      return {
                // bind to hotkey 
       'ctrl+shift+e' : this.remove
      }
    }
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
            process.env.baseUrl + '/api/getPeopleList',
            {"event_id" : this.$route.params.id})
        .then(response => {
          $nuxt.$loading.finish()
          this.events = response.data.res
        })
        .catch(function(error) {
          console.log(error)
        })
    },
    remove: function() {
      this.$router.push('/remove/' + this.$route.params.id)
    },
    back: function() {
      this.$router.go(-1)
    }
  }
}
</script>