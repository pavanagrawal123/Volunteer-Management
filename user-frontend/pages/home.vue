<template>
  <v-layout
    column
    justify-center
    align-center>
    <v-navigation-drawer permanent>
    <v-toolbar flat>
      <v-list>
        <v-list-tile>
          <v-list-tile-title class="title">
            Interact - {{this.$store.state.info.firstName}} {{this.$store.state.info.lastName}}
          </v-list-tile-title>
        </v-list-tile>
      </v-list>
    </v-toolbar>

    <v-divider></v-divider>

    <v-list dense class="pt-0">
      <v-list-tile
          v-for="(item, i) in items"
          :to="item.to"
          :key="i"
          router
          exact
        >
        <v-list-tile-action>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-tile-action>

        <v-list-tile-content>
          <v-list-tile-title>{{ item.title }}</v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>
  </v-layout>
</template>

<script>
import axios from 'axios'
import { mapMutations } from 'vuex'
import { mapFields } from 'vuex-map-fields'

export default {
  data: () => {
    return {
      items: [
          { icon: 'apps', title: 'Signup for events', to: '/signupEvents' },
          { icon: 'bubble_chart', title: 'View events I\'m signed up for', to: '/eventDetails' },
          { icon: 'exit_to_app', title: 'Logout', to: '/' }
        ],
    }
  },
  middleware: 'member',
  computed: {
    // The `mapFields` function takes an array of
    // field names and generates corresponding
    // computed properties with getter and setter
    // functions for accessing the Vuex store.
    ...mapFields(['info.firstName', 'info.lastName', 'info.grade'])
  },
  methods: {
    submit: function() {
      window.$nuxt.$loading.start()
      axios
        .post(process.env.baseUrl + '/api/getIfCurrent', {
          firstName: this.$store.info.firstName,
          lastName: this.$store.info.lastName,
          grade: this.$store.info.grade
        })
        .then(response => {
          window.$nuxt.$loading.end()
          if (response.data.res != false) {
            this.$store.commit('updateCurrent', true)
            this.$store.commit('updateUserID', response.data.res)
          } else {
            this.$store.commit('updateCurrent', false)
            this.$router.push('/register')
          }
        })
        .catch(function(error) {
          console.log(error)
        })
    }
  }
}
</script>