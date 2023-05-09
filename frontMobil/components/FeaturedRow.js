import { View, Text, ScrollView, LogBox } from "react-native";
import React, { useEffect, useState } from "react";
import { ArrowRightIcon } from "react-native-heroicons/outline";
import RestaurantCard from "./RestaurantCard";
import SanityClient from "../sanity";

const FeaturedRow = ({ title, id }) => {
  //const [restaurants, setRestaurants] = useState([]);
  const restaurants=[
    {
      _id:1,
      name:'Soltana Food',
      image:'https://scontent.ftun14-1.fna.fbcdn.net/v/t1.6435-9/105429256_117045336715598_3692495113764699504_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=e3f864&_nc_ohc=Y1Rx01PCITQAX8Q7iLJ&_nc_ht=scontent.ftun14-1.fna&oh=00_AfDg8CDZmxIQD7diK4nGleUY2Nu_rC1_K3eI6sXI8zig6w&oe=6471E5D9',
      short_description:'tunisian restaurant based in sfaxpreparation of popular Tunisian dishes',
      genre:'tunisien',
      address:'Sfax',
      dishes:[{id:1, name:'kosksi osben', short_description:'plat principale', price:'12', image:'https://pbs.twimg.com/media/FD5lJc4XwAACOWH.jpg:large'}],
      long:'',
      lat:'',
      rating:'4.8'
    }]

/*
  useEffect(() => {
    SanityClient.fetch(
      `
      
      *[_type == "featured" && _id == $id]{
        ...,
        restaurants[]->{
          ...,
        dishes[]->,
          type-> {
            name
          }
          
        }
     }[0]


      `,
      { id },
    ).then((data) => setRestaurants(data?.restaurants));
  }, [id]);
*/

  

  return (
    <View>
      <View className="mt-4 flex-row items-center justify-between px-4">
        <Text className="font-bold text-lg">{title}</Text>
        <ArrowRightIcon color="#00ccbb" />
      </View>


      <ScrollView
        showsHorizontalScrollIndicator={false}
        contentContainerStyle={{ paddingHorizontal: 15 }}
        horizontal
        className="pt-4"
      >
        {/* restaurant cards */}
        {restaurants?.map((restaurant) => (
          <RestaurantCard
            key={restaurant._id}
            id={restaurant._id}
            imgUrl={restaurant.image}
            title={restaurant.name}
            rating={restaurant.rating}
            genre={restaurant.type?.name}
            address={restaurant.address}
            short_description={restaurant.short_description}
            dishes={restaurant.dishes}
            long={restaurant.long}
            lat={restaurant.lat}
          />
        ))}
      </ScrollView>
    </View>
  );
};

export default FeaturedRow;
