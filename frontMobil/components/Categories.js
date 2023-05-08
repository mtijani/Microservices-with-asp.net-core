import { View, Text, ScrollView } from "react-native";
import React, { useEffect, useState } from "react";
import CategoryCard from "./CategoryCard";
import SanityClient, { urlFor } from "../sanity";
const Categories = () => {
  //const [categories, setCategories] = useState([]);
  const categories=[{_id:1,name:'plats principaux',image:'https://www.cap-voyage.com/wp-content/uploads/2019/09/Couscous-plats-tunisiens.jpg'},{_id:2,name:'soupe',image:'https://img.cuisineaz.com/660x660/2016/07/15/i71397-chorba-a-la-tunisienne.jpeg'},{_id:3,name:'salade',image:'https://www.kilometre-0.fr/wp-content/uploads/2019/07/20190715Cuisine_mart202.jpg'},{_id:4,name:'Dessert',image:'https://www.finedininglovers.fr/sites/g/files/xknfdk1291/files/styles/recipes_1200_800/public/fdl_content_import_scripts/Original_3996_makroudhs-patisseries-dattes-tunisie_0.jpg.webp?itok=qx9DiHXD'},{_id:5,name:'Boisson',image:'https://boutique.sofratel.fr/laon/wp-content/uploads/sites/17/2021/12/boissons33cl.jpg'},{_id:6,name:'Entrée',image:'https://voyage-tunisie.info/wp-content/uploads/2018/01/Le-Brik-en-demi-cercle.jpg'}]
  return (
    <ScrollView
      horizontal
      showsHorizontalScrollIndicator={false}
      contentContainerStyle={{
        paddingHorizontal: 15,
        paddingTop: 10,
      }}
    >
      {/* categories Card */}
      {categories.map((category) => (
        <CategoryCard
          imgUrl={category.image}
          key={category._id}
          title={category.name}
        />
      ))}
    </ScrollView>
  );
};

export default Categories;
