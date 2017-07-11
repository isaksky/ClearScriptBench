using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using Newtonsoft.Json;
using Microsoft.ClearScript;

namespace ClearscriptBench {
    public class MyTestClass {
        public int myInteger;
        public string myString;
        public MyNestedClass myNestedClass;
        public MyNestedClass[] myListOfNested;

        public MyTestClass(int myInt, string myString, MyNestedClass myNested, MyNestedClass[] myList) {
            this.myInteger = myInt;
            this.myString = myString;
            this.myNestedClass = myNested;
            this.myListOfNested = myList;
        }

        public MyTestClass() {

        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (MyTestClass)obj;
            var equals = 
                this.myInteger == other.myInteger 
                && this.myString == other.myString 
                && this.myNestedClass.Equals(other.myNestedClass)
                && this.myListOfNested.Length == other.myListOfNested.Length;

            if(!equals) { return false; }

            for (int i = 0; i < this.myListOfNested.Length; i++) {
                if (!this.myListOfNested[i].Equals(other.myListOfNested[i])) {
                    return false;
                }
            }
            return true;
        }
    }

    public class MyNestedClass {
        public float myFloat;
        public string myString;
        public int myInt;

        public MyNestedClass(float myFloat, string myString, int myInt) {
            this.myFloat = myFloat;
            this.myString = myString;
            this.myInt = myInt;
        }

        public MyNestedClass() { }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (MyNestedClass)obj;
            return
                this.myFloat == other.myFloat
                && this.myString == other.myString
                && this.myInt == other.myInt;
        }
    }

    public class JsonVsEmbed {
        V8JsEngine _engine = new V8JsEngine();

        MyTestClass _myTestObj;

        public JsonVsEmbed() {
            _engine.EmbedHostObject("host", new HostFunctions());
            _engine.EmbedHostType("MyTestClass", typeof(MyTestClass));
            _engine.EmbedHostType("MyNestedClass", typeof(MyNestedClass));

            _myTestObj =
                new MyTestClass(
                    5,
                    "Foo",
                    new MyNestedClass(5.0f, "Ocelot", 50),
                    new[] { new MyNestedClass(2.0f, "Cat", 20), new MyNestedClass(1.0f, "Mouse", 10) });
        } 

        [Benchmark]
        public MyTestClass Embed() {
            var ret = (MyTestClass)_engine.Evaluate(@"
                var myArr = host.newArr(MyNestedClass, 2);
                myArr[0] = host.newObj(MyNestedClass, 2.0, ""Cat"", 20);
                myArr[1] = host.newObj(MyNestedClass, 1.0, ""Mouse"", 10);                 
                host.newObj(MyTestClass,5, ""Foo"", host.newObj(MyNestedClass, 5.0, ""Ocelot"", 50), myArr)");
            //if (!ret.Equals(_myTestObj)) {
            //    throw new Exception("Not equal!");
            //}
            return ret;
        }

        [Benchmark]
        public MyTestClass Json() {
            var retStr = (string)_engine.Evaluate(@"JSON.stringify({ myInteger: 5, myString: ""Foo"", 
                myNestedClass: { myFloat: 5.0, myString: ""Ocelot"", myInt: 50},
                myListOfNested: [{ myFloat: 2.0, myString: ""Cat"", myInt: 20}, { myFloat: 1.0, myString: ""Mouse"", myInt: 10}] 
})");
            var ret =  Newtonsoft.Json.JsonConvert.DeserializeObject<MyTestClass>(retStr);
            //if (!ret.Equals(_myTestObj)) {
            //    throw new Exception("Not equal!");
            //}
            return ret;

        }
    }
}
