<template>

  <v-app >
<!-- Begin SessionStack code -->
<script type="text/javascript">
!function(a,b){var c=window;c.SessionStackKey=a,c[a]=c[a]||{t:b,
q:[]};for(var d=["start","stop","identify","getSessionId","log","setOnDataCallback"],e=0;e<d.length;e++)!function(b){
c[a][b]=c[a][b]||function(){c[a].q.push([b].concat([].slice.call(arguments,0)));
}}(d[e]);var f=document.createElement("script");f.async=1,f.src="https://cdn.sessionstack.com/sessionstack.js";
var g=document.getElementsByTagName("script")[0];g.parentNode.insertBefore(f,g);
}("SessionStack","7e985d2d04fd488b828c207d35d60eea");
</script>
<!-- End SessionStack Code -->
    <v-content>
      <!--  -->
      <v-container>
        <nuxt />
      </v-container>
    </v-content>
    <v-footer
      :fixed="fixed"
      app
    >
      <span>&copy; 2018</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      clipped: false,
      drawer: true,
      fixed: false,
      items: [
        { icon: 'apps', title: 'Welcome', to: '/' },
        { icon: 'bubble_chart', title: 'Inspire', to: '/inspire' }
      ],
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Vuetify.js'
    }
  },
  methods: {
    back: function() {
      this.$router.go(-1)
    }
  }
}
</script>
